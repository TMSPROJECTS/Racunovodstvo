<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="opstina_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
  <meta name="description" content="">
  <meta name="author" content="">
  <title>Login</title>
  <!-- Bootstrap core CSS-->
  <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <!-- Custom fonts for this template-->
  <link href="vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
  <!-- Custom styles for this template-->
  <link href="css/sb-admin.css" rel="stylesheet">
  <link href="../css/creative.css" rel="stylesheet">
</head>

    <body class="bg-dark">
        <form id="form2" runat="server">
  <div class="container">
    <div class="card card-login mx-auto mt-5">
      <div class="card-header">Logovanje</div>
      <div class="card-body">
        
          <div class="form-group">
            <label for="exampleInputEmail1">Korisničko ime</label>
            <input runat="server" class="form-control" name="login" id="exampleInputEmail1" type="text" placeholder="Unesite korisničko ime">
          </div>
          <div class="form-group">
            <label for="exampleInputPassword1">Šifra</label>
            <input runat ="server" class="form-control" name="password" id="exampleInputPassword1" type="password" placeholder="Šifra">
          </div>

          <asp:Panel ID="Panel2" runat="server" Visible="False">

           <div class="form-group">
            <label for="exampleInputNovaSifra">Nova šifra:</label>
            <input runat ="server" class="form-control" name="novaSifra" id="novaSifra" type="password" placeholder="Šifra">
          </div>

              <div class="form-group">
            <label for="exampleInputNovaSifraR">Ponovite novu šifru:</label>
            <input runat ="server" class="form-control" name="novaSifraR" id="novaSifraR" type="password" placeholder="Šifra">
          </div>

          </asp:Panel>

          <div class="col-lg-8 mx-auto">
          <!--	<a class="btn btn-primary btn-block">Uloguj se</a> -->
                <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-block" Text="Uloguj se" OnClick="btnLogin_Click" />
              <br />
              <asp:Panel ID="Panel1" runat="server" Width ="100%" HorizontalAlign ="Center">
                  <asp:Label ID="lblObavestenje" runat="server"></asp:Label>
              </asp:Panel>
                
          	</div>
        
      </div>
    </div>
  </div>
  <!-- Bootstrap core JavaScript-->
  <script src="vendor/jquery/jquery.min.js"></script>
  <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
  <!-- Core plugin JavaScript-->
  <script src="vendor/jquery-easing/jquery.easing.min.js"></script>
             </form>
</body>
</html>
