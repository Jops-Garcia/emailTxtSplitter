using System;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public partial class _Default : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileUpload.HasFile)
        {
            string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

            // verifica se é um .txt
            if (fileExtension != ".txt")
            {
                Response.Write("<script>alert('Erro: Apenas arquivos .txt são permitidos!');</script>");
                return;
            }
            
            try
            {
                // Lê o arquivo e add na lista
                List<string> emails = new List<string>();
                using (StreamReader reader = new StreamReader(fileUpload.PostedFile.InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        emails.Add(reader.ReadLine().Trim()); // Remove espacos
                    }
                }

                // Remove emails iguais, linhas em branco e add no HashSet
                HashSet<string> emailSet = new HashSet<string>(emails.Where(email => !string.IsNullOrEmpty(email)));

                // Binda no gridView
                gvEmails.DataSource = emailSet.Select(email => new { Email = email }).ToList();
                gvEmails.DataBind();

                // Diretorio para salvar os arquivos
                string path = Server.MapPath("~/ArquivosGerados/");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // Divide os e-mails em arquivos de 5 linhas cada
                // Usa o HashSet para dividir os emails em grupos de 5 se baseando no index Ex: o range de 0-4 dividido por 5 é 0, ou seja, grupo 0.
                List<string> createdFiles = new List<string>();
                int fileIndex = 1;
                foreach (var emailGroup in emailSet.Select((email, index) => new { email, index }).GroupBy(x => x.index / 5))
                {
                    //Converte o numero do arquivo em para 2 digitos ex: 1 -> 01
                    string fileName = fileIndex.ToString("D2") + ".txt";
                    string filePath = Path.Combine(path, fileName);

                    File.WriteAllLines(filePath, emailGroup.Select(x => x.email));
                    createdFiles.Add("ArquivosGerados/" + fileName);

                    fileIndex++;
                }

                // Exibe os links para download
                rtFiles.DataSource = createdFiles.Select(f => new { Path = f, FileName = Path.GetFileName(f) });
                rtFiles.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Erro: " + ex.Message + "');</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('Por favor, selecione um arquivo.');</script>");
        }
    }
}