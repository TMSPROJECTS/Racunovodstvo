using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class izborJedinice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((String)Session["odabranaGodina"] == null || (String)Session["odabranaGodina"] == "")
        {
            Response.Redirect("Index.aspx");
        }

        DataTable dtSveGodine = Upiti.Select("Naziv", "poslovnica", "ne");

        foreach (DataRow red in dtSveGodine.Rows)
        {
            Button btn = new Button();
            btn.ID = "ID" + red["Naziv"].ToString();
            btn.Text = red["Naziv"].ToString();
            btn.Click += new EventHandler(button_Click);
            btn.CssClass = "btn btn-light btn-xl";

            panelZaPoslovneJedinice.Controls.Add(btn);
            panelZaPoslovneJedinice.Controls.Add(new LiteralControl("&nbsp&nbsp"));
        }
    }

    protected void button_Click(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        Session["odabranaPoslovnica"] = button.Text;

        DataTable dtNazivBaze = Upiti.Select("NazivBaze", "poslovnica", "Naziv='" + button.Text + "'");
        foreach (DataRow red in dtNazivBaze.Rows)
        {
            Session["odabranaPoslovnicaBaza"] = red["NazivBaze"].ToString();
        }

        Response.Redirect("/opstina/login.aspx");

    }
}