// --------------------------------------------------------------------------------------------------------------------
// <summary>
// Classe de mock de serviço de teste
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ProdutosAPI.Testes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ProdutosApi.Models;
    using ProdutosAPI.Services;

    // <summary>
    // Classe de mock de serviço de teste
    // </summary>
    public class ProdutoServiceMock : IProdutoService
    {
        // <summary>
        // Lista de produtos
        // </summary>
        private readonly List<Produto> _produtos;

        // <summary>
        // construtor de mock de serviço de teste
        // </summary>
        public ProdutoServiceMock()
        {
            _produtos = new List<Produto>()
            {
                new Produto() { Id = "5dcf3ef3314bbb238e4e84aa", Nome = "TV", Descricao = "TV de 50 polegadas",
                    URLImagem = "https://http2.mlstatic.com/smart-tv-led-samsung-50-polegadas-ultra-hd-4k-wi-fi-3-hdmi-u-D_NQ_NP_987210-MLB31865906256_082019-F.jpg",
                    Categoria = "Televisão", Preco = 3500.00, Marca = "Sony"},

                new Produto() { Id = "5dcf3ef3314bbb238e4e84ab", Nome = "Controle", Descricao = "Controle de televisão",
                    URLImagem = "https://images-americanas.b2w.io/produtos/01/00/sku/9356/3/9356394_1SZ.jpg",
                    Categoria = "Controle", Preco = 199.90, Marca = "Samsung"},

                new Produto() { Id = "5dcf3ef3314bbb238e4e84ac", Nome = "TV", Descricao = "Smart TV NU7400 65 polegadas UHD 4K",
                    URLImagem = "https://images.samsung.com/is/image/samsung/br-uhdtv-nu7400-un65nu7400gxzd-frontblack-113032383?$PD_GALLERY_L_JPG$",
                    Categoria = "Televisão", Preco = 9299.90, Marca = "Samsung"}
            };
        }

        /// <summary> 
        /// Adiciona um produto
        /// </summary>
        /// <param name="produto">
        /// O produto
        /// </param>        
        /// <returns>
        /// Retorna o produto adicionado
        /// </returns>
        public Produto Adicionar(Produto produto)
        {
            produto.Id = Guid.NewGuid().ToString();
            _produtos.Add(produto);
            return produto;
        }

        /// <summary> 
        /// Obtém o produto por identificador
        /// </summary>
        /// <param name="id">
        /// Identificador do produto
        /// </param>        
        /// <returns>
        /// Retorna o produto consultado.
        /// </returns>
        public Produto ConsultarPorIdentificador(string id)
        {
            return _produtos.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary> 
        /// Edita um produto
        /// </summary>
        /// <param name="id">
        /// Identificador do produto a ser editado
        /// </param>        
        /// <param name="produtoAtualizado">
        /// Produto com informações atualizadas
        /// </param>
        public void Editar(string id, Produto produtoAtualizado)
        {
            var produto = _produtos.First(x => x.Id == produtoAtualizado.Id);
            _produtos.Remove(produto);
            _produtos.Add(produtoAtualizado);
        }

        /// <summary> 
        /// Exclui um produto
        /// </summary>
        /// <param name="produtoRemovido">
        /// Produto a ser excluido
        /// </param>                        
        public void Excluir(Produto produtoRemovido)
        {
            var produto = _produtos.First(x => x.Id == produtoRemovido.Id);
            _produtos.Remove(produto);
        }


        /// <summary> 
        /// Exclui um produto
        /// </summary>
        /// <param name="id">
        /// Identificador do produto a ser excluido
        /// </param>  
        public void Excluir(string id)
        {
            var produto = _produtos.First(x => x.Id == id);
            _produtos.Remove(produto);
        }

        /// <summary> 
        /// Executa o filtro
        /// </summary>
        /// <param name="nome"> 
        /// Obtém ou define o nome dos produtos.
        /// </param>
        /// <param name="categoria"> 
        /// Obtém ou define a categoria dos produtos.
        /// </param>
        /// <param name="marca"> 
        /// Obtém ou define a marca dos produtos.
        /// </param>
        /// <param name="preco"> 
        /// Obtém ou define o preço dos produtos.
        /// </param>
        /// <returns>
        /// Retorna a lista de produtos consultados.
        /// </returns>
        private static bool ConsultarComFiltro(Produto item, string nome, string categoria, string marca, double? preco)
        {
            bool consulta = false;

            if (nome != null) {
                consulta = (item.Nome == nome);
            }

            if (categoria != null)
            {
                consulta = (item.Categoria == categoria);
            }

            if (marca != null)
            {
                consulta = (item.Marca == marca);
            }

            if (preco > 0.00)
            {
                consulta = (item.Preco == preco);
            }

            return consulta;
        }

        /// <summary> 
        /// Obtém os produtos de forma paginada por nome e/ou por categoria e/ou marca e/ou preço
        /// </summary>
        /// <param name="limite">
        /// Limite de produtos por consulta.
        /// </param>
        /// <param name="pagina">
        /// Página.
        /// </param>        
        /// <param name="nome"> 
        /// Obtém ou define o nome dos produtos.
        /// </param>
        /// <param name="categoria"> 
        /// Obtém ou define a categoria dos produtos.
        /// </param>
        /// <param name="marca"> 
        /// Obtém ou define a marca dos produtos.
        /// </param>
        /// <param name="preco"> 
        /// Obtém ou define o preço dos produtos.
        /// </param>
        /// <returns>
        /// Retorna a lista de produtos consultados.
        /// </returns>
        public List<Produto> Consultar(int limite, int pagina, string nome, string categoria, string marca, double? preco)
        {         
            List<Produto> produtosEncontrados = _produtos;

            if (nome != null){
                produtosEncontrados = _produtos.Where(x => x.Nome == nome).ToList();
            }

            if (categoria != null)
            {
                produtosEncontrados = _produtos.Where(x => x.Categoria == categoria).ToList();
            }

            if (marca != null)
            {
                produtosEncontrados = _produtos.Where(x => x.Marca == marca).ToList();
            }

            if (preco > 0.00)
            {
                produtosEncontrados = _produtos.Where(x => x.Preco == preco).ToList();
            }
           
            return produtosEncontrados;
        }
    }
}
