Tema: Sistema de Gest�o de Portf�lio de Investimentos
Descri��o:
Voc� foi contratado para desenvolver um sistema de gest�o de portf�lio de investimentos para uma empresa de consultoria financeira. O sistema deve permitir que os usu�rios da opera��o gerenciem os investimentos dispon�veis e os clientes comprem, vendam e acompanhe seus investimentos.
Obs: O desenvolvimento deve ser realizado em C# e n�o h� a necessidade de implementar um front-end para a entrega em quest�o, somente os servi�os no backend que permitam a realiza��o das opera��es.
Requisitos:
Criar um servi�o que permita o time de opera��o realizar manuten��o nos produtos de investimentos.
Funcionalidades:
�	
o	Gest�o dos produtos financeiros
o	Disparo de e-mail di�rio para notificar os administradores a respeito dos produtos com vencimento pr�ximo
o	Criar um servi�o que permita o cliente comprar, vender e consultar seus investimentos.
Funcionalidades:
�	
o	Negociar produto financeiro (Compra e Venda)
o	Extrato do produto
O que esperamos:
�	
o	As funcionalidades de consulta de produtos dispon�veis e extrato devem suportar um grande volume de requisi��es e manter baixo tempo de resposta, abaixo de 100ms

- para suportar um grande volume de requisi��es
	- c�digo ass�ncrono 
	- adicionado cache response para garantir que respostas frequentes sejam rapidas e nao precisem executar nova requisi��o no banco
	- Pooling de Conex�es: Use o pool de conex�es do banco de dados para reutilizar conex�es abertas.
	- Compression: Comprimir respostas HTTP para reduzir a quantidade de dados transferidos.
	- Pagination: Implementar pagina��o em endpoints que retornam listas grandes de dados.

- criado testes integrados para testar responsta


o	Documenta��o de como executar a aplica��o
foi criadoum docker compose onde ao executar ira gerar o ambiente para rodar local
como banco de dados, e apps

o	Documenta��o de como utilizar a aplica��o
