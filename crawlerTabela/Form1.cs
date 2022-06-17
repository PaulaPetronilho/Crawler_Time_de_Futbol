using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace crawlerTabela
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            WebClient webClient  = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            webClient.Headers.Add("User-Agent: Others");

            string pagina = webClient.DownloadString("https://esportes.estadao.com.br/classificacao/futebol/campeonato-brasileiro-serie-a/2019");
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(pagina);
            var tabela = document.DocumentNode.SelectNodes("//table[@class='table-groups-times']/tbody");
            List < Tabela >  lstTabela = new List<Tabela>();

            foreach (var item in tabela[0].ChildNodes)
            {
               if(item.OriginalName != "#text")
                {
                    string posicao = item.ChildNodes[0].InnerText;
                    string time = item.ChildNodes[3].InnerText;
                    lstTabela.Add(new Tabela { Posicao = posicao, Time = time });
                }
            }
            dtgTabela.DataSource = lstTabela;
        }
    }
}
