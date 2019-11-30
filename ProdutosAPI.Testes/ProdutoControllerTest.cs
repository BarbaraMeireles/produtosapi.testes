// --------------------------------------------------------------------------------------------------------------------
// <summary>
// Testes de produtos
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ProdutosAPI.Testes
{
    using Microsoft.AspNetCore.Mvc;
    using ProdutosApi.Controllers;
    using ProdutosApi.Models;
    using ProdutosAPI.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    // <summary>
    // Testes de produtos
    // </summary>
    public class ProdutoControllerTest
    {
        ProdutosController _controller;
        IProdutoService _service;

        // <summary>
        // Construtor de testes de produtos
        // </summary>
        public ProdutoControllerTest()
        {
            _service = new ProdutoServiceMock();
            _controller = new ProdutosController(_service);
        }

        /// <summary>
        /// Teste: Deve retornar resultado OK ao consultar todos os produtos
        /// </summary>       
        [Fact]
        public void DeveRetornarResultadoOKAoConsultarTodosProdutos()
        {
            string nome = null;
            string categoria = null;
            string marca = null;
            double preco = 0;

            var okResult = _controller.Get(10, 1, nome, categoria, marca, preco);

            Assert.IsType<OkObjectResult>(okResult.Result);           
        }

        /// <summary>
        /// Teste: Deve retornar todos os produtos
        /// </summary>
        [Fact]
        public void DeveRetornarTodosProdutos()
        {
            string nome = null;
            string categoria = null;
            string marca = null;
            double preco = 0;

            var okResult = _controller.Get(10, 1, nome, categoria, marca, preco).Result as OkObjectResult;
            var items = Assert.IsType<List<Produto>>(okResult.Value);
            Assert.Equal(3, items.Count);            
        }

        /// <summary>
        /// Teste: Deve retornar todos os produtos com nome TV
        /// </summary>
        [Fact]
        public void DeveRetornarTodosProdutosComNomeTV()
        {
            string nome = "TV";
            string categoria = null;
            string marca = null;
            double preco = 0;

            var okResult = _controller.Get(10, 1, nome, categoria, marca, preco).Result as OkObjectResult;
            var items = Assert.IsType<List<Produto>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        /// <summary>
        /// Teste: Deve retornar todos os produtos com categoria controle
        /// </summary>
        [Fact]
        public void DeveRetornarTodosProdutosCategoriaControle()
        {
            string nome = null;
            string categoria = "Controle";
            string marca = null;
            double preco = 0;

            var okResult = _controller.Get(10, 1, nome, categoria, marca, preco).Result as OkObjectResult;
            var items = Assert.IsType<List<Produto>>(okResult.Value);
            Assert.Equal(1, items.Count);
        }

        /// <summary>
        /// Teste: Deve retornar todos os produtos com marca Samsung
        /// </summary>
        [Fact]
        public void DeveRetornarTodosProdutosMarcaSamsung()
        {
            string nome = null;
            string categoria = null;
            string marca = "Samsung";
            double preco = 0;

            var okResult = _controller.Get(10, 1, nome, categoria, marca, preco).Result as OkObjectResult;
            var items = Assert.IsType<List<Produto>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        /// <summary>
        /// Teste: Deve retornar todos os produtos com preco 199.90
        /// </summary>
        [Fact]
        public void DeveRetornarTodosProdutosComPreco199Reas90Centavos()
        {
            string nome = null;
            string categoria = null;
            string marca = null;
            double preco = 199.90;

            var okResult = _controller.Get(10, 1, nome, categoria, marca, preco).Result as OkObjectResult;
            var items = Assert.IsType<List<Produto>>(okResult.Value);
            Assert.Equal(1, items.Count);
        }

        /// <summary>
        /// Teste: Deve retornar resultado não encontrado ao consultar identificador inexistente
        /// </summary>
        [Fact]
        public void DeveRetornarResultadoNaoEncontradoAoConsultarIdentificadorInexistente()
        {
            var naoEncontrado = _controller.Get(Guid.NewGuid().ToString());            
            Assert.IsType<NotFoundResult>(naoEncontrado.Result);            
        }


        /// <summary>
        /// Teste: Deve retornar resultado OK ao consultar identificador existente
        /// </summary>
        [Fact]
        public void DeveRetornarResultadoOKAoConsultarIdentificadorExistente()
        {
            string id = "5dcf3ef3314bbb238e4e84aa";           
            var resultado = _controller.Get(id);
            Assert.IsType<OkObjectResult>(resultado.Result);
        }

        /// <summary>
        /// Teste: Deve retornar produto correto ao consultar identificador existente
        /// </summary>
        [Fact]
        public void DeveRetornarProdutoCorretoAoConsultarIdentificadorExistente()
        {
            string id = "5dcf3ef3314bbb238e4e84aa";
            var resultado = _controller.Get(id).Result as OkObjectResult;

            Assert.IsType<Produto>(resultado.Value);
            Assert.Equal(id, (resultado.Value as Produto).Id);
        }


        /// <summary>
        /// Teste: Deve retornar bad request ao adicionar objeto inválido
        /// </summary>
        [Fact]
        public void DeveRetornarBadRequestAoAdicionarObjetoInvalido()
        {
            var novoProduto = new Produto()
            {
                Descricao = "Videogame playstation 4 com HD de 1T",
                Marca = "Sony",
                Categoria = "Videogame",
                Preco = 2990.00
            };

            _controller.ModelState.AddModelError("Nome", "Required");

            var badRequest = _controller.Post(novoProduto);
            Assert.IsType<BadRequestObjectResult>(badRequest);            
        }

        /// <summary>
        /// Teste: Deve retornar resposta Created ao adicionar objeto válido
        /// </summary>
        [Fact]
        public void DeveRetornarRespostaCreatedAoAdicionarObjetoValido()
        {
            var novoProduto = new Produto()
            {
                Nome = "Nintendo Switch",
                Descricao = "Videogame Nintendo Switch Azul e Vermelho",
                Marca = "Nintendo",
                Categoria = "Videogame",
                Preco = 2049.99
            };

            var resposta = _controller.Post(novoProduto);

            Assert.IsType<CreatedAtActionResult>(resposta);            
        }

        /// <summary>
        /// Teste: Deve gravar novo produto
        /// </summary>
        [Fact]
        public void DeveGravarNovoProduto()
        {
            var novoProduto = new Produto()
            {
                Nome = "PlayStation 4",
                Descricao = "Videogame playstation 4 com HD de 1T",
                Marca = "Sony",
                Categoria = "Videogame",
                Preco = 2990.00
            };

            var resposta = _controller.Post(novoProduto) as CreatedAtActionResult;
            var item = resposta.Value as Produto;

            Assert.IsType<Produto>(item);
            Assert.Equal(novoProduto.Nome, item.Nome);
        }

        /// <summary>
        /// Teste: Deve retornar resposta not found ao remover produto inexistente
        /// </summary>
        [Fact]
        public void DeveRetornarRespostaNotFoundAoRemoverProdutoInexistente()
        {
            string idInexistente = Guid.NewGuid().ToString();
            var resposta = _controller.Delete(idInexistente);

            Assert.IsType<NotFoundResult>(resposta);
        }

        /// <summary>
        /// Teste: Deve retornar resposta OK ao remover produto existente
        /// </summary>
        [Fact]
        public void DeveRetornarRespostaOKAoRemoverProdutoExistente()
        {
            string id = "5dcf3ef3314bbb238e4e84ab";
            var resposta = _controller.Delete(id);

            Assert.IsType<OkResult>(resposta);
        }

        /// <summary>
        /// Teste: Deve remover um produto
        /// </summary>
        [Fact]
        public void DeveRemoverUmProduto()
        {
            string id = "5dcf3ef3314bbb238e4e84ac";
            var resposta = _controller.Delete(id);

            List<Produto> produtos = _service.Consultar(10, 1, null, null, null, 0);            

            Assert.Equal(2, produtos.Count());
        }

        /// <summary>
        /// Teste: Deve retornar resposta not found ao atualizar produto inexistente
        /// </summary>
        [Fact]
        public void DeveRetornarRespostaNotFoundAoAtualizarProdutoInexistente()
        {            
            string id = new Guid().ToString();

            var produtoAtualzado = new Produto()
            {
                Id = id,
                Nome = "TV 4k 3D",
                Descricao = "TV 4K 3D de 50 polegadas",
                URLImagem = "https://http2.mlstatic.com/smart-tv-led-samsung-50-polegadas-ultra-hd-4k-wi-fi-3-hdmi-u-D_NQ_NP_987210-MLB31865906256_082019-F.jpg",
                Marca = "Sony",
                Categoria = "Televisão",
                Preco = 3500.90
            };

            var resposta = _controller.Update(id, produtoAtualzado);
            Assert.IsType<NotFoundResult>(resposta);
        }

        /// <summary>
        /// Teste: Deve atualizar informações do produto
        /// </summary>
        [Fact]
        public void DeveAtualizarInformacaoProduto()
        {
            var produtoAtualzado = new Produto()
            {
                Id = "5dcf3ef3314bbb238e4e84aa",
                Nome = "TV 4k 3D",
                Descricao = "TV 4K 3D de 50 polegadas",
                URLImagem = "https://http2.mlstatic.com/smart-tv-led-samsung-50-polegadas-ultra-hd-4k-wi-fi-3-hdmi-u-D_NQ_NP_987210-MLB31865906256_082019-F.jpg",
                Marca = "Sony",
                Categoria = "Televisão",
                Preco = 3500.90
            };

            string id = "5dcf3ef3314bbb238e4e84aa";
            var resposta = _controller.Update(id, produtoAtualzado);
            Produto produto = _service.ConsultarPorIdentificador(id);
            Assert.Equal(produtoAtualzado.Nome, produto.Nome);
            Assert.Equal(produtoAtualzado.Descricao, produto.Descricao);
            Assert.Equal(produtoAtualzado.URLImagem, produto.URLImagem);
            Assert.Equal(produtoAtualzado.Marca, produto.Marca);
            Assert.Equal(produtoAtualzado.Categoria, produto.Categoria);
            Assert.Equal(produtoAtualzado.Preco, produto.Preco);
        }
    }
}
