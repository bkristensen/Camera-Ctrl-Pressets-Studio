[English](../en/DEVELOPMENT.md) · [Dansk](../da/DEVELOPMENT.md) · [Deutsch](../de/DEVELOPMENT.md)

# Edite a interface e as traduções

*Escrito para Camera Control Studio 1.1.0.*

## Abra o arquivo certo

Abra `Camera-Control-Pressets-Studio.sln`. No Solution Explorer, crie a solução e abra `CameraControlStudio → Forms → MainForm.cs`. Pressione **Shift+F7** para Windows Forms Designer. Não selecione `MainForm.Designer.cs` como superfície de design. Use F7 para retornar ao código.

Instale a carga de trabalho de desktop .NET Visual Studio de `.vsconfig`. O aplicativo é WinForms no .NET Framework 4.8. O designer WPF não é usado.

## Alterar layout

Use o painel Propriedades para alterar `Dock`, `Margin`, `Padding`, tamanhos de linha e tamanhos de coluna. Use o Document Outline para selecionar painéis aninhados. O layout principal utiliza tabelas e um `SplitContainer`; evite coordenadas de tela codificadas.

Cada janela herda de `AppForm` e possui um construtor público sem parâmetros. Coloque o layout em `.Designer.cs` e os eventos no arquivo `.cs` correspondente. Defina a propriedade `Icon` do formulário no Designer. Verifique o layout em Visual Studio e no aplicativo em execução.

A roda da câmera e os botões azuis são desenhados em `Controls/BlueControls.cs`. O divisor vertical e os controles de zoom estão em `Controls/MainViewControls.cs`. O designer pode mover e dimensionar esses controles. As imagens de ícones predefinidas e seus títulos neutros estão em `Assets/PressetIcons.resx` e `Controls/PresetIcons.cs` os carrega. A guia Vídeo contém controles deslizantes de autoria do designer. O aplicativo em execução mostra os controles deslizantes suportados pelo driver da câmera.

## Definir padrões

Edite `Shared/Models.cs`, em `SavedData.Defaults`, para configurações de novo perfil. Edite `ProfileLayout` para o layout inicial. Os perfis existentes mantêm os valores salvos. Crie um novo perfil para testar novos padrões; não exclua perfis de usuário para forçá-los.

## Atualizar uma tradução

Edite o valor correspondente no arquivo de idioma relevante em `Shared/Locales`. Mantenha o `key` inalterado. XML requer `&amp;` para um E comercial. Todos os arquivos devem ter as mesmas chaves. Mantenha os IDs de câmeras e ícones armazenados inalterados.

Execute `scripts/check-translations.ps1` e depois construa. Teste com `"Camera Control Studio.exe" --lang=de` ou com o seletor de idioma nas configurações. Reinicie após alterar a escolha de idioma salva.

## Adicionar um idioma

Copie `en.xml` para um novo arquivo de código de idioma. Traduza cada valor e registre o código e nome em `Shared/UiText.cs`. O verificador de tradução e os testes de localidade selecionam o novo arquivo e o registro automaticamente. Declare o idioma do aplicativo no manifesto MSIX.

A seleção automática usa o prefixo de idioma do Windows. Os idiomas não suportados voltam ao inglês. Teste janelas estreitas e etiquetas longas; a tradução não deve ocultar controles.
