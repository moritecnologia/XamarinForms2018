using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using App01_ConsultarCEP.Servico.Modelo;
using Newtonsoft.Json;

namespace App01_ConsultarCEP.Servico
{
    class ViaCEPServico
    {
        //Cria-se a propriedade privada do tipo string EnderecoURL que receberá o parâmetro em "{0}"
        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";

        //Cria-se o método BuscarEnderecoViaCEP com o parâmetro cep
        //Colocando abaixo Endereco, retornará um objeto do tipo Endereco, precisando utilizar 
        //a clase using App01_ConsultarCEP.Servico.Modelo;
        public static Endereco BuscarEnderecoViaCEP (string cep)
        {
            //A variável NovoEnderecoURL receberá o endereço, com o parâmetro cep enciado para a propriedade EnderecoURL
            string NovoEnderecoURL = string.Format(EnderecoURL, cep);

            //Utiliza-se a classe ou namespace using System.Net;
            //Assim será possível utilizar a classe WebClient
            WebClient wc = new WebClient();
            //Utilizando o método Síncrono DownloadString
            string Conteudo = wc.DownloadString(NovoEnderecoURL);

            //Precisará instalar a biblioteca do NuGet que é o Newtonsoft.Json e usar  using Newtonsoft.Json;
            //Neste abaixo com ajuda do JsonConvert.DeserializeObject vai obter do Conteudo a informação e converter em um objeto do tipo endereço
            Endereco end = JsonConvert.DeserializeObject<Endereco>(Conteudo);

            //Para tratar caso em que o cep não retorna o endereço, como exemplo 01000000
            if (end.cep == null) return null;

            //Retorna a variável endereço
            return end;
        }

    }
}
