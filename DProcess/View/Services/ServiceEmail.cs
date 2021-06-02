using Domino;
using Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using View.Resources;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace View.Services
{
    public class ServiceEmail
    {
        //var AttachME = nDocument.CreateRichTextItem("Attachment"); //agregado
        //var EmbedObj = AttachME.EmbedObject(1454, "", sAttachment, "Attachment");

        //public bool SendEmailLotus(string pathDBEmail, string[] recipients, string title, string body)
        //{
        //    try
        //    {
        //        string reci = string.Empty;

        //        for (int i = 0; i < recipients.Length - 1; i++)
        //        {
        //            reci += recipients[i] + ",";
        //        }

        //        int j = DataManager.InsertSolicitudCorreo(title, body, reci, origen, idArchivo);

        //        return j > 0 ? true : false;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    //try
        //    //{
        //    //    NotesSession nSession = new NotesSession();
        //    //    nSession.Initialize("");
        //    //    nSession.ConvertMime = false;
        //    //    string UserName = nSession.UserName;
        //    //    int indexstr = UserName.IndexOf(" ") + 1;
        //    //    string MailDbName = pathDBEmail;

        //    //    NotesDatabase nDatabase;
        //    //    nDatabase = nSession.GetDatabase(null, MailDbName, false);

        //    //    if (!nDatabase.IsOpen)
        //    //    {
        //    //        nDatabase.Open();
        //    //    }

        //    //    NotesDocument nDocument = nDatabase.CreateDocument();

        //    //    //setup Form
        //    //    nDocument.ReplaceItemValue("Form", "Memo");
        //    //    nDocument.ReplaceItemValue("SentTo", recipients);
        //    //    nDocument.ReplaceItemValue("Subject", title);
        //    //    nDocument.ReplaceItemValue("Body", body);
        //    //    nDocument.SaveMessageOnSend = true; //save message after it's sent
        //    //    nDocument.Send(false, recipients); //send

        //    //    return true;
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //}
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="pathDBEmail"></param>
        ///// <param name="recipients"></param>
        ///// <param name="title"></param>
        ///// <param name="body"></param>
        ///// <param name="files">Vector con las ubicaciones fisicas del archivo que se enviara.</param>
        ///// <returns></returns>
        //public Task<DO_PathMail> SendEmailWithAttachment(string pathDBEmail, string[] recipients, string title, string body, string[] files)
        //{
        //    return Task.Run(() =>
        //    {
        //        DO_PathMail Obj = new DO_PathMail();

        //        if (System.IO.File.Exists(pathDBEmail))
        //        {
        //            try
        //            {
        //                NotesSession nSession = new NotesSession();
        //                nSession.Initialize("");
        //                NotesStream stream = nSession.CreateStream();
        //                nSession.ConvertMime = false;
        //                NotesDatabase nDatabase;

        //                string MailDbName = pathDBEmail;
        //                nDatabase = nSession.GetDatabase(null, MailDbName, false);

        //                if (!nDatabase.IsOpen)
        //                {
        //                    nDatabase.Open();
        //                }

        //                var nDocument = nDatabase.CreateDocument();

        //                //var AttachME = nDocument.CreateRichTextItem("Attachment"); //agregado
        //                //foreach (string file in files)
        //                //{
        //                //    AttachME.EmbedObject(EMBED_TYPE.EMBED_ATTACHMENT, "Attachment", file, "Attachment");
        //                //}

        //                //setup Form
        //                nDocument.ReplaceItemValue("Form", "Memo");
        //                nDocument.ReplaceItemValue("SentTo", recipients);
        //                nDocument.ReplaceItemValue("Subject", title);

        //                NotesMIMEEntity sBody = nDocument.CreateMIMEEntity();
        //                NotesMIMEEntity chield = sBody.CreateChildEntity();

        //                stream.WriteText(body);

        //                chield.SetContentFromText(stream, "text/html;charset=iso-8859-1", MIME_ENCODING.ENC_IDENTITY_8BIT);
        //                stream.Close();
        //                stream.Truncate();

        //                nDocument.Send(false, recipients);

        //                Obj.respuesta = true;
        //                //Obj.rutamail = StringResources.msgCorreoEnviadoOK;
        //                Obj.rutamail = StringResources.msgCorreoEnviadoExitosamente;

        //                return Obj;
        //            }
        //            catch (Exception)
        //            {
        //                Obj.respuesta = false;
        //                Obj.rutamail = StringResources.msgErrorEnviarCorreo;

        //                return Obj;
        //            }
        //        }
        //        else
        //        {
        //            Obj.respuesta = false;
        //            Obj.rutamail = StringResources.msgDeseasConfigCorreo;

        //            return Obj;
        //        }
        //    });
        //}

        public bool SendEmailLotusCustom(string pathDBEmail, string[] recipients, string title, string body, string[] files, string origen, int idArchivo)
        {
            try
            {
                string reci = string.Empty;

                for (int i = 0; i < recipients.Length; i++)
                {
                    reci += recipients[i] + ",";
                }

                int j = DataManager.InsertSolicitudCorreo(title, body, reci, origen, idArchivo);

                return j > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }

            //try
            //{
            //    NotesSession nSession = new NotesSession();
            //    nSession.Initialize("");
            //    NotesStream stream = nSession.CreateStream();
            //    nSession.ConvertMime = false;
            //    NotesDatabase nDatabase;
            //    string MailDbName = pathDBEmail;
            //    nDatabase = nSession.GetDatabase(null, MailDbName, false);

            //    if (!nDatabase.IsOpen)
            //    {
            //        nDatabase.Open();
            //    }

            //    var nDocument = nDatabase.CreateDocument();
            //    var AttachME = nDocument.CreateRichTextItem("Attachment"); //agregado

            //    foreach (string file in files)
            //    {
            //        AttachME.EmbedObject(EMBED_TYPE.EMBED_ATTACHMENT, "Attachment", file, "Attachment");
            //    }

            //    //setup Form
            //    nDocument.ReplaceItemValue("Form", "Memo");
            //    nDocument.ReplaceItemValue("SentTo", recipients);
            //    nDocument.ReplaceItemValue("Subject", title);

            //    NotesMIMEEntity sBody = nDocument.CreateMIMEEntity();
            //    NotesMIMEEntity chield = sBody.CreateChildEntity();

            //    stream.WriteText(body);

            //    chield.SetContentFromText(stream, "text/html;charset=iso-8859-1", MIME_ENCODING.ENC_NONE);
            //    stream.Close();
            //    stream.Truncate();

            //    nDocument.Send(false, recipients);

            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        /// <summary>
        /// Envío de correo en documentos.
        /// </summary>
        /// <param name="pathDBEmail"></param>
        /// <param name="recipients"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public bool SendEmailLotusCustom(string pathDBEmail, string[] recipients, string title, string body, string origen, int idArchivo)
        {
            try
            {
                string reci = string.Empty;

                for (int i = 0; i < recipients.Length; i++)
                {
                    reci += recipients[i] + ",";
                }

                int j = DataManager.InsertSolicitudCorreo(title, body, reci, origen, idArchivo);

                return j > 0 ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
            //try
            //{
            //    NotesSession nSession = new NotesSession();
            //    nSession.Initialize("");
            //    NotesStream stream = nSession.CreateStream();
            //    nSession.ConvertMime = false;
            //    NotesDatabase nDatabase;

            //    string MailDbName = pathDBEmail;

            //    try
            //    {
            //        nDatabase = nSession.GetDatabase(null, MailDbName, false);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }

            //    if (!nDatabase.IsOpen)
            //    {
            //        nDatabase.Open();
            //    }

            //    var nDocument = nDatabase.CreateDocument();

            //    //setup Form
            //    nDocument.ReplaceItemValue("Form", "Memo");
            //    nDocument.ReplaceItemValue("SentTo", recipients);
            //    nDocument.ReplaceItemValue("Subject", title);

            //    NotesMIMEEntity sBody = nDocument.CreateMIMEEntity();
            //    NotesMIMEEntity chield = sBody.CreateChildEntity();

            //    stream.WriteText(body);

            //    chield.SetContentFromText(stream, "text/html;charset=iso-8859-1", MIME_ENCODING.ENC_NONE);
            //    stream.Close();
            //    stream.Truncate();

            //    nDocument.Send(false, recipients);

            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        public bool SendEmailOutlook(string[] recipients, string title, string body, List<string> attachments)
        {
            Outlook.Application application = new Outlook.Application();
            Outlook.NameSpace ns = application.GetNamespace("MAPI");

            try
            {
                Object selectedObject = application.ActiveExplorer().Selection[1];
                //Outlook.MailItem selectedMail = (Outlook.MailItem)selectedObject;

                Outlook.MailItem newMail = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

                for (int i = 0; i < recipients.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(recipients[i]))
                    {
                        newMail.Recipients.Add(recipients[i]);
                    }
                }

                newMail.Subject = title;
                //newMail.Attachments.Add(selectedMail, Microsoft.Office.Interop.Outlook.OlAttachmentType.olEmbeddeditem);
                newMail.HTMLBody = body;
                foreach (var recipient in attachments)
                {
                    newMail.Attachments.Add(recipient, Outlook.OlAttachmentType.olByValue);
                }

                newMail.Send();

                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }
    }
}