using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using TP_MS_VENDA;


namespace Servicos
{
    public interface IServVenda
    {
        void Inserir(InserirVendaDTO inserirVendaDto);
        public void Extornar(int id, Boolean ExtornarVenda);
        public List<EntidadeVenda> BuscarTodos();
    }

    public class ServVenda : IServVenda
    {
        private DataContext _dataContext;
        public ServVenda(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Inserir(InserirVendaDTO inserirVendaDto)
        {

            var httpClient = new HttpClient();

            var url = "https://localhost:7214/api/Evento/BuscarEvento";

            var data = new
            {
                EventoId = inserirVendaDto.EventoId,
                Quantidade = inserirVendaDto.Quantidade
            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Console.WriteLine(json);

            var result = httpClient.PostAsync(url, content).Result;
            string? responseContent = result.Content.ReadAsStringAsync().Result;

            if (!result.IsSuccessStatusCode)
            {

                throw new Exception(responseContent);
            }
            else
            {
                EntidadeVenda venda = new EntidadeVenda()
                {
                    CompradorId = inserirVendaDto.CompradorId,
                    EventoId = inserirVendaDto.EventoId,
                    Quantidade = inserirVendaDto.Quantidade
                };
                _dataContext.Add(venda);
                _dataContext.SaveChanges();
            }                
        }

        public void Extornar(int id, Boolean ExtornarVenda)
        {
            var httpClient = new HttpClient();

            var url = "https://localhost:7214/api/Evento/AtualizaIngressos";
            
            EntidadeVenda vendaExistente = _dataContext.Venda.FirstOrDefault(venda => venda.Id == id);
            var QuantidadeAdicionar = vendaExistente.Quantidade * -1;

            var data = new
            {
                EventoId = vendaExistente.EventoId,
                Quantidade = QuantidadeAdicionar
            };

            

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            Console.WriteLine(json);
            Console.WriteLine(content);

            var result = httpClient.PutAsync(url, content).Result;
            string? responseContent = result.Content.ReadAsStringAsync().Result;

            Console.WriteLine("teste");

            Console.WriteLine(result);

            if (!result.IsSuccessStatusCode)
            {

                throw new Exception(responseContent);
            }
            else
            {

                vendaExistente.Estornado = ExtornarVenda;

                _dataContext.Update(vendaExistente);

                _dataContext.SaveChanges();
            }

            //_servEvento.AtualizaIngressos(vendaExistente.EventoId, QuantidadeAdicionar);
        }

        public List<EntidadeVenda> BuscarTodos()
        {
            var venda = _dataContext.Venda.ToList();

            return venda;

            /*
            var httpClient = new HttpClient();

            var url = "https://localhost:7145/api/Comprador";

            var result = httpClient.GetAsync(url).Result;

            var content = result.Content.ReadAsStringAsync().Result;

            return content;
          */


        }
    }
}
