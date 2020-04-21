using System;
using System.Linq;
using System.Text;
using ShareLib.SharepadData;

/* Database initialisation command:
 * CREATE TABLE Text (
 *   TextID nchar(32) NOT NULL PRIMARY KEY,
 *   TextData nvarchar(max) NOT NULL,
 *   CreationTime Datetime NOT NULL,
 *   AccessTime Datetime NOT NULL
 *   )
 */

namespace ShareLib
{
    public class Sharepad
    {
        /* Properties. */
        private Random RandomGenerator { get; }
        private string Characters { get; }
        private TimeSpan TextLifetime { get; }
        private int TextIDSize { get; }

        /* Constructor. */
        public Sharepad(int LifeTimeInDays)
        {
            /* This generator is fast but not cryptographically secure. */
            this.RandomGenerator = new Random();
            /* Alphanumeric ASCII character set with "1" and "l" removed. */
            this.Characters = "023456789abcdefghijkmnopqrstuvwxyz";
            /* Lifetime of the Text entries. */
            this.TextLifetime = new TimeSpan(LifeTimeInDays, 0, 0, 0);
            /* Length of the TextID. */
            this.TextIDSize = 32;
        }

        /* Methods. */
        /* Removes old entries from the database. */
        public void CleanDatabase()
        {
            SharepadContext DatabaseContext = new SharepadContext();
            DateTime ExpirationTime = DateTime.Now - this.TextLifetime;
            DatabaseContext.Text.RemoveRange(
                from Text in DatabaseContext.Text
                where Text.AccessTime.CompareTo(ExpirationTime) < 0 ||
                    Text.TextData.Length == 0
                select Text
                );
            DatabaseContext.SaveChanges();
        }

        /* Creates a new TextID. */
        public string CreateID()
        {
            StringBuilder TextID = new StringBuilder("", this.TextIDSize);
            for (int i = 0; i < this.TextIDSize; i++)
            {
                int Index = this.RandomGenerator.Next(0, this.Characters.Length);
                TextID.Append(this.Characters[Index]);
            }
            return TextID.ToString();
        }

        /* Creates a new database entry with a random TextID. */
        public string CreateText()
        {
            SharepadContext DatabaseContext = new SharepadContext();
            string TextID = this.CreateID();
            Text NewText = new Text();
            NewText.TextId = TextID;
            NewText.TextData = "";
            NewText.CreationTime = DateTime.Now;
            NewText.AccessTime = NewText.CreationTime;
            DatabaseContext.Text.Add(NewText);
            DatabaseContext.SaveChanges();
            return TextID;
        }

        /* Reads a database entry with given TextID. */
        public string ReadText(string TextID)
        {
            SharepadContext DatabaseContext = new SharepadContext();
            Text OutText = DatabaseContext.Text.Find(TextID);
            if (OutText is null)
            {
                throw new ArgumentException($"Text with ID '{TextID}' not found.");
            }
            /* Set the access time and update the database. */
            OutText.AccessTime = DateTime.Now;
            DatabaseContext.Update(OutText);
            DatabaseContext.SaveChanges();
            return OutText.TextData;
        }

        /* Writes given TextData to the database. */
        public void WriteText(string TextID, string TextData)
        {
            SharepadContext DatabaseContext = new SharepadContext();
            Text UpdateText = DatabaseContext.Text.Find(TextID);
            if (UpdateText is null)
            {
                throw new ArgumentException($"Text with ID '{TextID}' not found.");
            }
            UpdateText.TextData = TextData;
            UpdateText.AccessTime = DateTime.Now;
            DatabaseContext.Update(UpdateText);
            DatabaseContext.SaveChanges();
        }
    }
}