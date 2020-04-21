using System;
using System.Text;
using ShareLib.SharepadData;

/* Database initialisation command:
 * CREATE TABLE Text (
 *   TextID char(32) NOT NULL PRIMARY KEY,
 *   TextData ntext NOT NULL,
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

        /* Constructor. */
        public Sharepad()
        {
            /* This generator is fast but not cryptographically secure. */
            this.RandomGenerator = new Random();
            /* Alphanumeric ASCII character set with "1" and "l" removed. */
            this.Characters = "023456789abcdefghijkmnopqrstuvwxyz";
        }

        /* Methods. */
        /* Creates a new TextID. */
        public string CreateID()
        {
            StringBuilder TextID = new StringBuilder("", 32);
            for (int i = 0; i < 32; i++)
            {
                int Index = this.RandomGenerator.Next(0, this.Characters.Length);
                TextID.Append(this.Characters[Index]);
            }
            /* Todo. Create a list of chars and an RNG. Draw random indices and build a string. */
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
            UpdateText.TextData = TextData;
            UpdateText.AccessTime = DateTime.Now;
            DatabaseContext.Update(UpdateText);
            DatabaseContext.SaveChanges();
        }
    }
}
