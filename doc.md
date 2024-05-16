Tema: Sistema de Gestão de Portfólio de Investimentos
Descrição:
Você foi contratado para desenvolver um sistema de gestão de portfólio de investimentos para uma empresa de consultoria financeira. O sistema deve permitir que os usuários da operação gerenciem os investimentos disponíveis e os clientes comprem, vendam e acompanhe seus investimentos.
Obs: O desenvolvimento deve ser realizado em C# e não há a necessidade de implementar um front-end para a entrega em questão, somente os serviços no backend que permitam a realização das operações.
Requisitos:
Criar um serviço que permita o time de operação realizar manutenção nos produtos de investimentos.
Funcionalidades:
•	
o	Gestão dos produtos financeiros
o	Disparo de e-mail diário para notificar os administradores a respeito dos produtos com vencimento próximo
o	Criar um serviço que permita o cliente comprar, vender e consultar seus investimentos.
Funcionalidades:
•	
o	Negociar produto financeiro (Compra e Venda)
o	Extrato do produto
O que esperamos:
•	
o	As funcionalidades de consulta de produtos disponíveis e extrato devem suportar um grande volume de requisições e manter baixo tempo de resposta, abaixo de 100ms

- para suportar um grande volume de requisições
	- código assíncrono 
	- adicionado cache response para garantir que respostas frequentes sejam rapidas e nao precisem executar nova requisição no banco
	- Pooling de Conexões: Use o pool de conexões do banco de dados para reutilizar conexões abertas.
	- Compression: Comprimir respostas HTTP para reduzir a quantidade de dados transferidos.
	- Pagination: Implementar paginação em endpoints que retornam listas grandes de dados.

- criado testes integrados para testar responsta


o	Documentação de como executar a aplicação
foi criadoum docker compose onde ao executar ira gerar o ambiente para rodar local
como banco de dados, e apps

o	Documentação de como utilizar a aplicação
