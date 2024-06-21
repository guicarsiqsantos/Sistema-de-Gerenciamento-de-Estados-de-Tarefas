# 🖥️ Sistema-de-Gerenciamento-de-Estados-de-Tarefas
Os alunos devem criar um sistema de gerenciamento de tarefas em C# usando o padrão State, permitindo que tarefas mudem de estado (Criado, Em Progresso, Concluído, Cancelado) via endpoints RESTful. O projeto deve seguir boas práticas de programação orientada a objetos.

## 🚨 Requisitos do Sistema

### Padrão State
- Implementar padrão State para estados de tarefas.
- Estados: Criado, Em Progresso, Concluído, Cancelado.
- Classe Task com id, name, description.

### Persistência de Dados
- Banco de dados relacional.
- Utilização do Entity Framework.

### Endpoints RESTful
- POST /tasks (Cria a tarefa)
- Estados:
<br> GET /Task/{id} (Pesquisa)
<br> PUT /Task/{id}/Começar (Iniciar a tarefa)
<br> PUT /Task/{id}/Completo (Conclui a tarefa)
<br> PUT /Task/{id}/Cancelado (Cancela a tarefa)
