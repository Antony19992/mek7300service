## Instalação e Configuração

1. **Compile o Projeto**
   - Certifique-se de compilar o projeto e gerar o arquivo executável (`MEK7300service.exe`).

2. **Abra o Prompt de Comando como Administrador**
   - Clique com o botão direito no ícone do Prompt de Comando e selecione **Executar como Administrador**.

3. **Crie o Serviço**
   Execute o comando abaixo para criar o serviço no Windows:
   ```cmd
   sc create mek7300listener binPath= "C:\CAMINHO\PARA\MEK7300service.exe"
```
