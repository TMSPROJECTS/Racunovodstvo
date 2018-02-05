using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtSveGodine = Upiti.Select("Godina", "godine", "ne");

        foreach (DataRow red in dtSveGodine.Rows)
        {
            Button btn = new Button();
            btn.ID = "ID" + red["Godina"].ToString();
            btn.Text = "-- " + red["Godina"].ToString () + " --";
            btn.Click += new EventHandler(button_Click);
            btn.CssClass = "btn btn-light btn-xl";

            panelZaGodine.Controls.Add(btn);
            panelZaGodine.Controls.Add(new LiteralControl("&nbsp&nbsp"));
            
        }
    }

    protected void button_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        string godina = button.Text.Remove(0, 3);
        godina = godina.Remove(4, 3);

        Session["odabranaGodina"] = godina;

        Response.Redirect("izborJedinice.aspx");

    }
}