
# GerenciamentoCurso API

Esta API é responsável pelo gerenciamento de cursos, alunos, turmas e matrículas, oferecendo uma solução robusta para controle acadêmico. Ela implementa **controle de acesso baseado em roles**, garantindo que operações sensíveis sejam realizadas apenas por usuários autorizados (**Administradores**).

---

## 🔑 Autenticação e Autorização

Esta API utiliza um sistema de autenticação (provavelmente JWT ou Basic Auth) para proteger seus endpoints.

### Acesso Administrativo (Role `ADM`)

Para manipular entidades sensíveis como **`Aluno`**, **`Turma`** e **`Matrícula`**, você precisará de um usuário com a role de **Administrador (`ADM`)**.

#### Obtendo Credenciais `ADM`

Existem duas formas de garantir que você tenha um usuário com privilégios de `ADM`:

1.  **Via `dump.sql` (Primeira Configuração):**
    A forma de começar é executando o arquivo **`dump.sql`** em seu banco de dados. Este script **provisionará um usuário com a role `ADM`** que você poderá usar para seu primeiro login e para criar outros usuários `ADM` via API.

2.  **Via API (Requer Autenticação `ADM` Prévia):**
    O `UsuarioController` (`api/Usuario`) é responsável por criar, atualizar e deletar usuários. No entanto, este controlador é protegido com `[Authorize(Roles = "Adm")]`.
    Isso significa que, para criar um novo usuário (incluindo um usuário ADM) através da rota `[HttpPost()]` do `UsuarioController`, **você já precisa estar autenticado com um token de um usuário `ADM` existente**.
