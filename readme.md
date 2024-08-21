# Sistema de Gestão de Portfólio de Investimentos

## Visão Geral

Este documento descreve o sistema de gestão de portfólio de investimentos, que permite a gestão de produtos financeiros e a negociação de investimentos por clientes, desenvolvido para uma empresa de consultoria financeira.

## Arquitetura do Sistema

### Tecnologia Utilizada
- **Linguagem:** C#
- **Framework:** .NET 6.0
- **Banco de Dados:** SQL Server
- **ORM:** Entity Framework Core
- **Envio de E-mail:** SMTP com integração via FluentEmail
- **Testes:** xUnit

### Estrutura do Projeto

- **Case.Dominio:** Contém as entidades e interfaces.
- **Case.Data:** Contém o contexto de dados e configurações do Entity Framework Core.
- **Case.Repositorios:** Implementação das interfaces de repositórios.
- **Case.Servicos:** Implementação dos serviços de negócio, incluindo o serviço de notificação por e-mail.
- **Case.API:** Exposição dos serviços via API RESTful.
- **Case.Testes:** Conjunto de testes unitários e de integração.


## Pré-requisitos
- .NET 6.0 SDK instalado
- SQL Server configurado

## Configuração de Envio de E-mails
O sistema está configurado para enviar e-mails via Mailtrap. Caso necessite, você pode alterar o username e password no arquivo de configuração `appsettings.json`:

```json
"SmtpSettings": {
  "Host": "sandbox.smtp.mailtrap.io",
  "Port": 2525,
  "EnableSsl": true,
  "UserName": "#UserName#",
  "Password": "#Password#"
}