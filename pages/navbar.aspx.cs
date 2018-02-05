using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data;
using MySql.Data.MySqlClient;



public partial class pages_navbar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ProveraSesije();

        if (!IsPostBack)
        {
            lblJedinica .Text = (String)Session["odabranaPoslovnica"];
         
          
        }
    }

    public void ProveraSesije()
    {

        string Korisnik = (String)Session["korisnickoIme"];
        string aktivnaSesija = (String)Session["aktivnaSesija"];

        bool jeAktivna = ProveriSesiju.ProveriAktivnuSesiju(Korisnik, aktivnaSesija);
        if (jeAktivna == false)
        {
            Response.Redirect("/opstina/login.aspx");
            return;
        }
    }

    protected void logout_click(object sender, EventArgs e)
    {
        //Session["korisnickoIme"] = null;
        Response.Redirect("/opstina/login.aspx");
    }


    protected void Timer1_Tick(object sender, EventArgs e)
    {
        ProveraSesije();
    }
}