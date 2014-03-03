using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Gallerie_Labb2_Emil_k.Model;
using System.IO;
using System.Drawing;

namespace Gallerie_Labb2_Emil_k
{
    public partial class _default : System.Web.UI.Page
    {
        public bool HasSuccessMessage
        {
            get
            {
                return Session["SuccessMessage"] as string != null;
            }
        }
            private string SuccessMessage
            {
                get
                {
                    var message = Session["SuccessMessage"] as string;
                    Session.Remove("SuccessMessage");
                    return message;
                }
                set
                {
                     Session["SuccessMessage"] = value;
                }
            }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HasSuccessMessage)
            {
                successPlaceHolder.Visible = true;
                successLabel.Text = SuccessMessage;
            }
            var imageName = Request.QueryString["name"] as string;
            if (!String.IsNullOrWhiteSpace(imageName))
            {
                BigImage.ImageUrl = "Pictures/" + imageName;
                BigImage.Visible = true;
            }
        }

        protected void uploadButton_Click(object sender, EventArgs e)
        {
            if (chooseFileUpload.HasFile)
            {
               string fileName = Path.GetFileName(chooseFileUpload.PostedFile.FileName);
               var stream = chooseFileUpload.FileContent;
               Gallery hej = new Gallery();
               var name = Gallery.SaveImage(stream, fileName);
               
               SuccessMessage = String.Format("Uppladdningen av {0} lyckades", name);
               
               Response.Redirect("~/", false);
               Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                //var fileinfo = (Fileinfo)e.Item.Dataitem;
            }
        }
        public IEnumerable<string> Repeater1_GetData()
        {
            return Gallery.GetImageNames();
        }

    }
}