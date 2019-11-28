using Domino;
using Model;
using System;
using System.Threading.Tasks;

namespace View.Services
{
    public class ServiceEmail
    {
        //var AttachME = nDocument.CreateRichTextItem("Attachment"); //agregado

        //var EmbedObj = AttachME.EmbedObject(1454, "", sAttachment, "Attachment");
        public bool SendEmailLotus(string pathDBEmail, string[] recipients, string title, string body)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDBEmail"></param>
        /// <param name="recipients"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <param name="files">Vector con las ubicaciones fisicas del archivo que se enviara.</param>
        /// <returns></returns>
        public Task<DO_PathMail> SendEmailWithAttachment(string pathDBEmail, string[] recipients, string title, string body, string[] files)
        {
            return Task.Run(() =>
            {
                DO_PathMail Obj = new DO_PathMail();

                if (System.IO.File.Exists(pathDBEmail))
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
                        var AttachME = nDocument.CreateRichTextItem("Attachment"); //agregado

                        foreach (string file in files)
                        {
                            AttachME.EmbedObject(EMBED_TYPE.EMBED_ATTACHMENT, "Attachment", file, "Attachment");
                        }

                        //setup Form
                        nDocument.ReplaceItemValue("Form", "Memo");
                        nDocument.ReplaceItemValue("SentTo", recipients);
                        nDocument.ReplaceItemValue("Subject", title);

                        NotesMIMEEntity sBody = nDocument.CreateMIMEEntity();
                        NotesMIMEEntity chield = sBody.CreateChildEntity();

                        stream.WriteText(body);

                        chield.SetContentFromText(stream, "text/html;charset=iso-8859-1", MIME_ENCODING.ENC_IDENTITY_8BIT);
                        stream.Close();
                        stream.Truncate();

                        nDocument.Send(false, recipients);

                        Obj.respuesta = true;
                        Obj.rutamail = "Correo enviado exitosamente";

                        return Obj;
                    }
                    catch (Exception er)
                    {
                        Obj.respuesta = false;
                        Obj.rutamail = "ocurrió error al enviar el correo.";

                        return Obj;
                    }
                }
                else
                {
                    Obj.respuesta = false;
                    Obj.rutamail = "Tu correo no está configurado, ¿deseas configurarlo?";

                    return Obj;
                }
            });
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

                NotesMIMEEntity sBody = nDocument.CreateMIMEEntity();
                NotesMIMEEntity chield = sBody.CreateChildEntity();

                stream.WriteText(body);

                chield.SetContentFromText(stream, "text/html;charset=iso-8859-1", MIME_ENCODING.ENC_NONE);
                stream.Close();
                stream.Truncate();

                nDocument.Send(false, recipients);

                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }
    }
}
