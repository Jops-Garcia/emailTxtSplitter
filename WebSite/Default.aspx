<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Teste Prático Slabware</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css"/>

    <!-- css -->
    <style>
        body {
            background-color: #f8f9fa;
        }
        .container {
            max-width: 800px;
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            margin-top: 50px;
        }
        .btn-upload {
            width: 100%;
        }
        .table {
            margin-top: 20px;
        }
        .download-link {
            display: block;
            text-decoration: none;
            padding: 10px;
            margin: 5px 0;
            background-color: #007bff;
            color: white;
            text-align: center;
            border-radius: 5px;
            transition: 0.3s;
        }
        .download-link:hover {
            background-color: #0056b3;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>Faça upload do arquivo TXT</h2>
            
            <!-- form para envio do txt -->
            <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control mb-2" />
            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Enviar" OnClick="btnUpload_Click" />
            
            <hr />

            <!-- Emaisl coletados do arquivo -->
            <h3>Lista de E-mails</h3>
            <asp:GridView ID="gvEmails" runat="server" CssClass="table table-bordered"></asp:GridView>

            <hr />

            <!-- Links para download -->
            <h3>Arquivos</h3>
            <asp:Repeater ID="rtFiles" runat="server">
                <ItemTemplate>
                    <a href='<%# Eval("Path") %>' download='<%# Eval("FileName") %>'>
                        <%# Eval("FileName") %>
                    </a><br />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
