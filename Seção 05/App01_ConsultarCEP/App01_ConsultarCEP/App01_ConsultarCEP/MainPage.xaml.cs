using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico.Modelo;
using App01_ConsultarCEP.Servico;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            /*Se o BuscarCEP ficar com erro porque não atende ao padrão do EventHandler, precisando de 2 parâmetros */
            BOTAO.Clicked += BuscarCEP;

        }

        /*Usar o using App01_ConsultarCEP.Servico.Modelo; e o using App01_ConsultarCEP.Servico que foi criado antes*/
        private void BuscarCEP(object sender, EventArgs args)
        {

            //TODO - Validação
            string cep = CEP.Text.Trim();

            if (isValidCEP(cep))//TODO - Validação
            {
                //TODO - Lógica do Programa

                try//try-catch = Lida com as exceções ou erros, poderá fazer um teste comentando a parte do if(cep.Length != 8)
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);


                    if (end != null)//Este verifica se o cep não está nulo obtido de ViaCEPServico.cs em if (end.cep == null) return null;
                    {
                        RESULTADO.Text = String.Format("Endereço: {2} de {3} {0}, {1}", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }

                }
                catch (Exception e)
                {
                    DisplayAlert("ERRO CRÍTICO", e.Message, "OK");
                }
            }

        }

        //TODO - Validação
        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if(cep.Length != 8)
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve conter 8 caracteres.", "OK");
                valido = false;
            }

            int NovoCEP = 0;
            //TryParse = converte a string em integer de 32-bits
            if (!int.TryParse(cep,out NovoCEP))
            {
                //ERRO
                DisplayAlert("ERRO", "CEP Inválido! O CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }

            return valido;
        }
    }
}
