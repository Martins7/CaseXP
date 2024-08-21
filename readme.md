# Sistema de Gest�o de Portf�lio de Investimentos

## Vis�o Geral

Este documento descreve o sistema de gest�o de portf�lio de investimentos, que permite a gest�o de produtos financeiros e a negocia��o de investimentos por clientes, desenvolvido para uma empresa de consultoria financeira.

## Arquitetura do Sistema

### Tecnologia Utilizada
- **Linguagem:** C#
- **Framework:** .NET 6.0
- **Banco de Dados:** SQL Server
- **ORM:** Entity Framework Core
- **Envio de E-mail:** SMTP com integra��o via FluentEmail
- **Testes:** xUnit

### Estrutura do Projeto

- **Case.Dominio:** Cont�m as entidades e interfaces.
- **Case.Data:** Cont�m o contexto de dados e configura��es do Entity Framework Core.
- **Case.Repositorios:** Implementa��o das interfaces de reposit�rios.
- **Case.Servicos:** Implementa��o dos servi�os de neg�cio, incluindo o servi�o de notifica��o por e-mail.
- **Case.API:** Exposi��o dos servi�os via API RESTful.
- **Case.Testes:** Conjunto de testes unit�rios e de integra��o.


## Pr�-requisitos
- .NET 6.0 SDK instalado
- SQL Server configurado

## Configura��o de Envio de E-mails
O sistema est� configurado para enviar e-mails via Mailtrap. Caso necessite, voc� pode alterar o username e password no arquivo de configura��o `appsettings.json`:

```json
"SmtpSettings": {
  "Host": "sandbox.smtp.mailtrap.io",
  "Port": 2525,
  "EnableSsl": true,
  "UserName": "#UserName#",
  "Password": "#Password#"
}