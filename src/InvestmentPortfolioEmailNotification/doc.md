# Publicar o Projeto

Abra o projeto no Visual Studio e publique no seu azure

# Configurar Configura��es no Portal Azure

Ap�s a implanta��o, v� para o portal do Azure.
Navegue at� a Fun��o App que voc� acabou de criar.
Em Configura��es de Aplicativo, adicione as configura��es de e-mail que estavam em local.settings.json.

# Como Funciona
- Timer Trigger: A fun��o � executada diariamente �s 9h UTC.
- Servi�o de Notifica��o por E-mail: EmailNotificationService verifica produtos que expiram em at� 7 dias e chama EmailSender para enviar notifica��es.
- Envio de E-mails: EmailSender utiliza os par�metros de configura��o para se conectar ao servidor SMTP e enviar e-mails.

# Como Usar

- Configura��o Inicial:

Certifique-se de que todas as configura��es de e-mail est�o corretas e que a fun��o Azure est� implantada e em execu��o.

- Verifica��o de Produtos:

A fun��o ir� automaticamente verificar produtos de investimento que est�o pr�ximos do vencimento diariamente.

- Recebimento de E-mails:

Os administradores cadastrados (configura��o do destinat�rio do e-mail) receber�o e-mails sobre produtos que expiram em breve.

- Monitoramento e Logs:

Acompanhe os logs de execu��o da fun��o pelo portal do Azure para garantir que os e-mails est�o sendo enviados corretamente.
