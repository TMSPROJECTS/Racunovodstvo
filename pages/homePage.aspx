<%@ Page Language="C#" AutoEventWireup="true" CodeFile="homePage.aspx.cs" Inherits="homePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html;charset=UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
 <link href="../css/sb-adimin.css" rel="stylesheet"><title></title>
</head>
<body>
    
    <div>
    
    </div>
    
</body>
</html>



<body class="fixed-nav sticky-footer bg-dark" id="page-top">
    <form id="form1" runat="server">
<section class="bg-primary" id="about">
      <div class="container">
        <div class="row">
          <div class="col-lg-8 mx-auto text-center">
               <% 
                   string naziv = (String)Session["odabranaPoslovnica"];

                   System.Data.DataTable dtSVI = Upiti.Select("ID", "poslovnica", "naziv='" + naziv + "'");

                   int id = 0;

                   foreach (System.Data.DataRow redara in dtSVI.Rows)
                   {
                       id = int.Parse(redara["ID"].ToString());
                   }

                   System.Data.DataTable dtSviPodaci = Upiti.Select3("*", "obavestenja", "IDpj='" + id + "' and Redosled=0");



                   foreach (System.Data.DataRow red in dtSviPodaci.Rows)
                   {
                       Response.Write("<h2 class=section-heading text-white>" + red["NaslovObavestenja"].ToString () + "</h2>");
                       Response.Write("<hr class=light my - 4>");
                       Response.Write("<p class=text-faded mb-4>" + red["Obavestenje"].ToString() + "</p>");
                   }



                %>
            
          </div>
        </div>
      </div>
    </section>
      <div class="card mb-3">
       <div class="card-header">
          <i class="fa fa-table"></i> <b>Obaveštenja</b></div>
          <div class="card-body">
          
      <div class="row">

                <% 

                    System.Data.DataTable dtSviPodaci2 = Upiti.Select3("*", "obavestenja", "IDpj='" + id + "' and Redosled<>0 order by Redosled asc");

                     

                    foreach (System.Data.DataRow red in dtSviPodaci2.Rows)
                    {
                        Response.Write("<div class='col-xl-3 col-sm-6 mb-3'>");
                        Response.Write("<div class='card text-white bg-primary o-hidden h-100'>");
                        Response.Write("<div class='card-body'>");
                        Response.Write("<div class='card-body-icon'>");
                        Response.Write("<i class='fa fa-fw fa-comments'></i>");
                        Response.Write("</div>");
                        Response.Write("<div class='mr-5'>" + red["Obavestenje"].ToString() + "</div>");
                        Response.Write("</div>");
                        Response.Write("<a class='card-footer text-white clearfix small z-1' href=#>");
                        Response.Write("<span class='float-left'>" + red["NaslovObavestenja"].ToString () +"</span>");
                        Response.Write("</a>");
                        Response.Write("</div>");
                        Response.Write("</div>");
                    }

                     System.Data.DataTable dtSviPodaci3 = Upiti.Select3("*", "obavestenja", "IDpj='0' and Redosled<>0 order by Redosled asc");

                    foreach (System.Data.DataRow red in dtSviPodaci3.Rows)
                    {
                        Response.Write("<div class='col-xl-3 col-sm-6 mb-3'>");
                        Response.Write("<div class='card text-white bg-primary o-hidden h-100'>");
                        Response.Write("<div class='card-body'>");
                        Response.Write("<div class='card-body-icon'>");
                        Response.Write("<i class='fa fa-fw fa-comments'></i>");
                        Response.Write("</div>");
                        Response.Write("<div class='mr-5'>" + red["Obavestenje"].ToString() + "</div>");
                        Response.Write("</div>");
                        Response.Write("<a class='card-footer text-white clearfix small z-1' href=#>");
                        Response.Write("<span class='float-left'>" + red["NaslovObavestenja"].ToString () +"</span>");
                        Response.Write("</a>");
                        Response.Write("</div>");
                        Response.Write("</div>");
                    }

           %>

       

      </div>
    </div>
          
          </div>
         </div>
        </form>
</body>
</html>
