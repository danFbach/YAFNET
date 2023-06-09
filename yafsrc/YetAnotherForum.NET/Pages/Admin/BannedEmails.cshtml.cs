/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2023 Ingo Herbote
 * https://www.yetanotherforum.net/
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * https://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

namespace YAF.Pages.Admin;

using System.Collections.Generic;
using System.IO;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using YAF.Core.Extensions;
using YAF.Core.Helpers;
using YAF.Types.Extensions;
using YAF.Types.Modals;
using YAF.Types.Models;

/// <summary>
/// The Admin Banned Emails Page.
/// </summary>
public class BannedEmailsModel : AdminPage 
{
    [BindProperty]
    public List<BannedEmail> List { get; set; }

    [BindProperty]
    public string SearchInput { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BannedEmailsModel"/> class. 
    /// </summary>
    public BannedEmailsModel()
        : base("ADMIN_BANNEDEMAIL", ForumPages.Admin_BannedEmails)
    {
    }

    /// <summary>
    /// Creates page links for this page.
    /// </summary>
    public override void CreatePageLinks()
    {
        this.PageBoardContext.PageLinks.AddAdminIndex();

        this.PageBoardContext.PageLinks.AddLink(this.GetText("ADMIN_BANNEDEMAIL", "TITLE"), string.Empty);
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    public void OnGet()
    {
        this.PageSizeList = new SelectList(StaticDataHelper.PageEntries(), nameof(SelectListItem.Value), nameof(SelectListItem.Text));
        this.BindData();
    }

    public IActionResult OnPostDelete(int id)
    {
        this.GetRepository<BannedEmail>().DeleteById(id);

        this.BindData();

        return this.PageBoardContext.Notify(
            this.GetText("ADMIN_BANNEDEMAIL", "MSG_REMOVEBAN_EMAIL"),
            MessageTypes.success);
    }

    public PartialViewResult OnGetImport()
    {
        return new PartialViewResult {
                                         ViewName = "Dialogs/BannedEmailImport",
                                         ViewData = new ViewDataDictionary<ImportModal>(
                                             this.ViewData,
                                             new ImportModal())
                                     };
    }

    public PartialViewResult OnGetAdd()
    {
        return new PartialViewResult {
                                         ViewName = "Dialogs/BannedEmailEdit",
                                         ViewData = new ViewDataDictionary<BannedEmailEditModal>(
                                             this.ViewData,
                                             new BannedEmailEditModal { Id = 0 })
                                     };
    }

    public PartialViewResult OnGetEdit(int id)
    {
        // Edit
        var banned = this.GetRepository<BannedEmail>().GetById(id);

        return new PartialViewResult {
                                         ViewName = "Dialogs/BannedEmailEdit",
                                         ViewData = new ViewDataDictionary<BannedEmailEditModal>(
                                             this.ViewData,
                                             new BannedEmailEditModal {
                                                                          Id = banned.ID,
                                                                          Mask = banned.Mask, BanReason = banned.Reason
                                                                      })
                                     };
    }

    /// <summary>
    /// Handles the Click event of the Search control.
    /// </summary>
    public void OnPostSearch()
    {
        this.BindData();
    }

    /// <summary>
    /// The page size on selected index changed.
    /// </summary>
    public void OnPost()
    {
        this.BindData();
    }

    /// <summary>
    /// Export Banned Email(s)
    /// </summary>
    /// <returns>IActionResult.</returns>
    public IActionResult OnPostExport()
    {
        var bannedEmails = this.GetRepository<BannedEmail>().GetByBoardId();

        const string FileName = "BannedEmailsExport.txt";

        var stream = new MemoryStream();
        var streamWriter = new StreamWriter(stream);

        bannedEmails.ForEach(
            email =>
            {
                streamWriter.Write(email.Mask);
                streamWriter.Write(streamWriter.NewLine);
            });

        streamWriter.Close();

        return this.File(stream.ToArray(), "application/vnd.text", FileName);
    }

    /// <summary>
    /// Binds the data.
    /// </summary>
    private void BindData()
    {
        var searchText = this.SearchInput;

        List<BannedEmail> bannedList;

        if (searchText.IsSet())
        {
            bannedList = this.GetRepository<BannedEmail>().GetPaged(
                x => x.BoardID == this.PageBoardContext.PageBoardID && x.Mask == searchText,
                this.PageBoardContext.PageIndex,
                this.Size);
        }
        else
        {
            bannedList = this.GetRepository<BannedEmail>().GetPaged(
                x => x.BoardID == this.PageBoardContext.PageBoardID,
                this.PageBoardContext.PageIndex,
                this.Size);
        }

        this.List = bannedList;
    }
}