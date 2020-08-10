using Domino;
using Model;
using System;
using System.Threading.Tasks;
using View.Resources;

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

                        //string html = "<!Doctype html>";
                        //html += "<html lang=\"en\">";
                        //html += "<head>";
                        //html += "<meta charset=\"utf - 8\"> ";
                        //html += "<meta name = \"viewport\" content = \"width = device - width, initial - scale = 1, shrink - to - fit = no\" > ";
                        //html += "<link rel = \"stylesheet\" href = \"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css\"> ";
                        //html += "<title> Hello, world!</title>";
                        //html += "</head>";
                        //html += "<body>";
                        //html += "<button type=\"button\" onclick=\"decirHola();\" class=\"btn btn-primary\">Primary</button>";
                        //html += "<h1> Hello, world!</h1>";
                        //html += "<script src=\"https://code.jquery.com/jquery-3.5.1.slim.min.js\"></script>";
                        //html += "<script src=\"https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js\"></script>";
                        //html += "<script src=\"https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js\"></script>";
                        //html += "<script type=\"text/javascript\">";
                        //html += "function decirHola(){ alert(\"Hola mundo\"); }";
                        //html += "</script>";
                        //html += "</body>";
                        //html += "</html>";
                        stream.WriteText(body);
                        //stream.WriteText(html);
                        
                        chield.SetContentFromText(stream, "text/html;charset=iso-8859-1", MIME_ENCODING.ENC_IDENTITY_8BIT);
                        stream.Close();
                        stream.Truncate();

                        nDocument.Send(false, recipients);

                        Obj.respuesta = true;
                        //Obj.rutamail = StringResources.msgCorreoEnviadoOK;
                        Obj.rutamail = StringResources.msgCorreoEnviadoExitosamente;

                        return Obj;
                    }
                    catch (Exception er)
                    {
                        Obj.respuesta = false;
                        Obj.rutamail = StringResources.msgErrorEnviarCorreo;

                        return Obj;
                    }
                }
                else
                {
                    Obj.respuesta = false;
                    Obj.rutamail = StringResources.msgDeseasConfigCorreo;

                    return Obj;
                }
            });
        }

        public bool SendEmailLotusCustom(string pathDBEmail, string[] recipients, string title, string body, string[] files)
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
