using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace BrioPresets {
 public static class StartupSettingsDialog {
  static bool AskCode(IWin32Window owner,string title,out string code) {
   using(var f=new Form {Text=title,ClientSize=new Size(320,105),FormBorderStyle=FormBorderStyle.FixedDialog,StartPosition=FormStartPosition.CenterParent,MinimizeBox=false,MaximizeBox=false}) {
    var input=new TextBox {Left=12,Top=15,Width=296,UseSystemPasswordChar=true,MaxLength=64};
    var ok=new Button {Text="OK",Left=145,Top=58,Width=75,DialogResult=DialogResult.OK};
    var cancel=new Button {Text=UiText.T("Cancel","Annuller"),Left=230,Top=58,Width=78,DialogResult=DialogResult.Cancel};
    f.Controls.AddRange(new Control[]{input,ok,cancel});f.AcceptButton=ok;f.CancelButton=cancel;
    bool accepted=f.ShowDialog(owner)==DialogResult.OK;code=input.Text;return accepted;
   }
  }
  public static bool Unlock(Form owner,SavedData current) {
   if(!SettingsCode.IsSet(current))return true;
   string code;if(!AskCode(owner,UiText.T("Enter settings code","Indtast indstillingskode"),out code))return false;
   if(SettingsCode.Verify(current,code))return true;
   MessageBox.Show(owner,UiText.T("Incorrect code.","Forkert kode."));return false;
  }
  public static bool Edit(Form owner,SavedData current,out SavedData edited,out bool runNow) {
   edited=null;runNow=false;
   if(!Unlock(owner,current))return false;
   var data=ProfileStore.Copy(current);
   using(var f=new Form {Text=UiText.T("Profile settings — startup programs","Profilindstillinger — opstartsprogrammer"),ClientSize=new Size(800,470),MinimumSize=new Size(620,430),StartPosition=FormStartPosition.CenterParent}) {
    var layout=new TableLayoutPanel {Dock=DockStyle.Fill,Padding=new Padding(10),ColumnCount=1,RowCount=6};
    foreach(int h in new[]{30,58,0,38,47,40})layout.RowStyles.Add(h==0?new RowStyle(SizeType.Percent,100):new RowStyle(SizeType.Absolute,h));
    var auto=new CheckBox {Text=UiText.T("Check and start programs when this profile is opened","Kontrollér og start programmer, når denne profil åbnes"),Checked=data.AutoStartPrograms,AutoSize=true};layout.Controls.Add(auto,0,0);
    layout.Controls.Add(new Label {Dock=DockStyle.Fill,Text=UiText.T("Order: top to bottom. Already running programs are skipped. Enter process names without .exe (use Task Manager > Details). For Store apps, select a shortcut and enter the actual process name.","Rækkefølge: øverst til nederst. Programmer, der allerede kører, springes over. Procesnavne angives uden .exe (se Jobliste > Detaljer). For Store-apps vælges en genvej og det faktiske procesnavn.")},0,1);
    var grid=new DataGridView {Dock=DockStyle.Fill,AllowUserToAddRows=false,AllowUserToDeleteRows=false,RowHeadersVisible=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect,MultiSelect=false,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill};
    grid.Columns.Add("name",UiText.T("Name","Navn"));grid.Columns.Add("file",UiText.T("Executable / shortcut","Programfil / genvej"));grid.Columns.Add("args",UiText.T("Arguments","Argumenter"));grid.Columns.Add("process",UiText.T("Process name","Procesnavn"));grid.Columns[1].FillWeight=180;
    foreach(DataGridViewColumn col in grid.Columns)col.SortMode=DataGridViewColumnSortMode.NotSortable;
    foreach(var p in data.StartupPrograms)grid.Rows.Add(p.Name,p.FileName,p.Arguments,p.ProcessName);layout.Controls.Add(grid,0,2);
    var row=new FlowLayoutPanel {Dock=DockStyle.Fill,WrapContents=false};
    Action<string,Action> add=(text,action)=>{var b=new Button {Text=text,Width=115,Height=28};b.Click+=(s,e)=>action();row.Controls.Add(b);};
    add(UiText.T("Add program","Tilføj program"),()=>{if(grid.Rows.Count>=30){MessageBox.Show(f,UiText.T("Maximum 30 programs.","Højst 30 programmer."));return;}
     using(var pick=new OpenFileDialog {Filter=UiText.T("Programs and shortcuts|*.exe;*.lnk;*.appref-ms","Programmer og genveje|*.exe;*.lnk;*.appref-ms"),DereferenceLinks=false})if(pick.ShowDialog(f)==DialogResult.OK){string title=Path.GetFileNameWithoutExtension(pick.FileName);grid.Rows.Add(title,pick.FileName,"",Path.GetExtension(pick.FileName).Equals(".exe",StringComparison.OrdinalIgnoreCase)?title:"");}});
    add(UiText.T("Remove","Fjern"),()=>{if(grid.CurrentRow!=null)grid.Rows.RemoveAt(grid.CurrentRow.Index);});
    Action<int> move=delta=>{if(grid.CurrentRow==null)return;grid.EndEdit();int from=grid.CurrentRow.Index,to=from+delta;if(to<0||to>=grid.Rows.Count)return;
     for(int col=0;col<4;col++){object temp=grid.Rows[from].Cells[col].Value;grid.Rows[from].Cells[col].Value=grid.Rows[to].Cells[col].Value;grid.Rows[to].Cells[col].Value=temp;}grid.CurrentCell=grid.Rows[to].Cells[0];};
    add(UiText.T("Move up","Flyt op"),()=>move(-1));add(UiText.T("Move down","Flyt ned"),()=>move(1));layout.Controls.Add(row,0,3);
    var codeRow=new FlowLayoutPanel {Dock=DockStyle.Fill,WrapContents=true};var codeButton=new Button {Text=UiText.T("Set / change code","Sæt / skift kode"),Width=145,Height=28};
    var note=new Label {AutoSize=true,Text=UiText.T("Optional edit lock; not Windows security.","Valgfri redigeringslås; ikke Windows-sikkerhed.")};
    codeButton.Click+=(s,e)=>{string code;if(!AskCode(f,UiText.T("New code (blank removes it)","Ny kode (tom fjerner koden)"),out code))return;
     try{if(code.Length>0){string confirm;if(!AskCode(f,UiText.T("Repeat new code","Gentag ny kode"),out confirm))return;if(code!=confirm)throw new ArgumentException(UiText.T("The codes do not match.","Koderne er ikke ens."));}
      SettingsCode.Set(data,code);note.Text=SettingsCode.IsSet(data)?UiText.T("Code set. Click Save to keep it.","Kode sat. Tryk Gem for at beholde den."):UiText.T("Code removed. Click Save to keep the change.","Kode fjernet. Tryk Gem for at beholde ændringen.");
     }catch(Exception ex){MessageBox.Show(f,ex.Message);}};
    codeRow.Controls.Add(codeButton);codeRow.Controls.Add(note);layout.Controls.Add(codeRow,0,4);
    var bottom=new FlowLayoutPanel {Dock=DockStyle.Fill,FlowDirection=FlowDirection.RightToLeft,WrapContents=false};
    var cancelButton=new Button {Text=UiText.T("Cancel","Annuller"),Width=90,Height=30,DialogResult=DialogResult.Cancel};bottom.Controls.Add(cancelButton);
    Action<string,DialogResult> saveButton=(title,result)=>{var b=new Button {Text=title,Width=155,Height=30};b.Click+=(s,e)=>{try{grid.EndEdit();var programs=new List<StartupProgram>();foreach(DataGridViewRow r in grid.Rows){var p=new StartupProgram {Name=Convert.ToString(r.Cells[0].Value).Trim(),FileName=Convert.ToString(r.Cells[1].Value).Trim(),Arguments=Convert.ToString(r.Cells[2].Value),ProcessName=Convert.ToString(r.Cells[3].Value).Trim()};StartupLogic.Validate(p);programs.Add(p);}data.StartupPrograms=programs;data.AutoStartPrograms=auto.Checked;f.DialogResult=result;}catch(Exception ex){MessageBox.Show(f,ex.Message);}};bottom.Controls.Add(b);};
    saveButton(UiText.T("Save","Gem"),DialogResult.OK);saveButton(UiText.T("Save and start now","Gem og start nu"),DialogResult.Yes);layout.Controls.Add(bottom,0,5);f.Controls.Add(layout);f.CancelButton=cancelButton;
    DialogResult chosen=f.ShowDialog(owner);if(chosen!=DialogResult.OK&&chosen!=DialogResult.Yes)return false;edited=data;runNow=chosen==DialogResult.Yes;return true;
   }
  }
 }
}
