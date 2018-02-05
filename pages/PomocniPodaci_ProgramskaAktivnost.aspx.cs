using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;



public partial class pages_PomocniPodaci_ProgramskaAktivnost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)

        {
            Session["papa"] = Request.QueryString["SIFRA12"];
        }
    }
}