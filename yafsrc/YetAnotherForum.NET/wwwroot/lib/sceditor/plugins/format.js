/* SCEditor for YAF.NET v4.0.0-rc.2 | (C) 2024, Sam Clarke | sceditor.com/license */
!function(r){"use strict";r.plugins.format=function(){var a={p:"Paragraph",h1:"Heading 1",h2:"Heading 2",h3:"Heading 3",h4:"Heading 4",h5:"Heading 5",h6:"Heading 6",address:"Address",pre:"Preformatted Text"},o=(this.init=function(){var e=this.opts,t=e.paragraphformat;e.format&&"bbcode"===e.format||(t&&(t.tags&&(a=t.tags),t.excludeTags)&&t.excludeTags.forEach(function(e){delete a[e]}),this.commands.format||(this.commands.format={exec:o,txtExec:o,tooltip:"Format Paragraph"}),e.toolbar===r.defaultOptions.toolbar&&(e.toolbar=e.toolbar.replace(",color,",",color,format,")))},function(e){var n=this,t=document.createElement("div");r.utils.each(a,function(o,r){var e=document.createElement("a");e.className="sceditor-option dropdown-item",e.textContent=r.name||r,e.addEventListener("click",function(e){var t,a;n.closeDropDown(!0),r.exec?r.exec(n):(a=o,(t=n).sourceMode()?t.insert(`<${a}>`,`</${a}>`):t.execCommand("formatblock",`<${a}>`)),e.preventDefault()}),t.appendChild(e)}),n.createDropDown(e,"format",t)})}}(sceditor);