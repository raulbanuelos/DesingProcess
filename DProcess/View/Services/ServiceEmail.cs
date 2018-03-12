using Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services
{
    public class ServiceEmail
    {
        public bool SendEmailLotus(string pathDBEmail, string[] recipients,string title,string body)
        {
            try
            {
                NotesSession nSession = new NotesSession();
                nSession.Initialize("");
                nSession.ConvertMime = false;
                string UserName = nSession.UserName;
                int indexstr = UserName.IndexOf(" ") + 1;
                string MailDbName = pathDBEmail;

                NotesDatabase nDatabase;
                nDatabase = nSession.GetDatabase(null, MailDbName, false);

                if (!nDatabase.IsOpen)
                {
                    nDatabase.Open();
                }

                NotesDocument nDocument = nDatabase.CreateDocument();
                
                //setup Form
                nDocument.ReplaceItemValue("Form", "Memo");
                nDocument.ReplaceItemValue("SentTo", recipients);
                nDocument.ReplaceItemValue("Subject", title);
                nDocument.ReplaceItemValue("Body", body);
                nDocument.SaveMessageOnSend = true; //save message after it's sent
                
                nDocument.Send(false, recipients); //send

                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }

        public bool SendEmailLotusCustom(string pathDBEmail, string[] recipients, string title, string body)
        {
            try
            {
                NotesSession nSession = new NotesSession();
                nSession.Initialize("");
                NotesStream stream = nSession.CreateStream();
                nSession.ConvertMime = false;
                NotesDatabase nDatabase;
                string MailDbName = pathDBEmail;
                nDatabase = nSession.GetDatabase(null, MailDbName, false);

                if (!nDatabase.IsOpen)
                {
                    nDatabase.Open();
                }

                var nDocument = nDatabase.CreateDocument();
                //setup Form
                nDocument.ReplaceItemValue("Form", "Memo");
                nDocument.ReplaceItemValue("SentTo", recipients);
                nDocument.ReplaceItemValue("Subject", title);
                
                NotesMIMEEntity sBody =  nDocument.CreateMIMEEntity();
                NotesMIMEEntity chield = sBody.CreateChildEntity();
                stream.WriteText(body);
                chield.SetContentFromText(stream, "text/html;charset=iso-8859-1", MIME_ENCODING.ENC_NONE);
                stream.Close();
                stream.Truncate();
                
                nDocument.Send(false,recipients);


                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }
    }
}
