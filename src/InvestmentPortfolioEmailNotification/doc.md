# Publicar o Projeto

Abra o projeto no Visual Studio e publique no seu azure

# Configurar Configurações no Portal Azure

Após a implantação, vá para o portal do Azure.
Navegue até a Função App que você acabou de criar.
Em Configurações de Aplicativo, adicione as configurações de e-mail que estavam em local.settings.json.

# Como Funciona
- Timer Trigger: A função é executada diariamente às 9h UTC.
- Serviço de Notificação por E-mail: EmailNotificationService verifica produtos que expiram em até 7 dias e chama EmailSender para enviar notificações.
- Envio de E-mails: EmailSender utiliza os parâmetros de configuração para se conectar ao servidor SMTP e enviar e-mails.

# Como Usar

- Configuração Inicial:

Certifique-se de que todas as configurações de e-mail estão corretas e que a função Azure está implantada e em execução.

- Verificação de Produtos:

A função irá automaticamente verificar produtos de investimento que estão próximos do vencimento diariamente.

- Recebimento de E-mails:

Os administradores cadastrados (configuração do destinatário do e-mail) receberão e-mails sobre produtos que expiram em breve.

- Monitoramento e Logs:

Acompanhe os logs de execução da função pelo portal do Azure para garantir que os e-mails estão sendo enviados corretamente.
