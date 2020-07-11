/* Yet Another Forum.NET
 * Copyright (C) 2003-2005 Bjørnar Henden
 * Copyright (C) 2006-2013 Jaben Cargman
 * Copyright (C) 2014-2020 Ingo Herbote
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

namespace YAF.Dialogs
{
    #region Using

    using System;

    using YAF.Configuration;
    using YAF.Core.BaseControls;
    using YAF.Core.Utilities;
    using YAF.Types;
    using YAF.Types.Constants;
    using YAF.Types.Interfaces;
    using YAF.Types.Interfaces.Identity;
    using YAF.Types.Models.Identity;
    using YAF.Utils;

    #endregion

    /// <summary>
    /// The Login Box
    /// </summary>
    public partial class LoginBox : BaseUserControl
    {
        #region Methods

        /// <summary>
        /// The On PreRender event.
        /// </summary>
        /// <param name="e">
        /// the Event Arguments
        /// </param>
        protected override void OnPreRender([NotNull] EventArgs e)
        {
            // setup jQuery and YAF JS...
            this.PageContext.PageElements.RegisterJsBlock(
                "yafmodaldialogJs",
                JavaScriptBlocks.LoginBoxLoadJs(".LoginLink", "#LoginBox"));

            base.OnPreRender(e);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load([NotNull] object sender, [NotNull] EventArgs e)
        {
            if (this.IsPostBack)
            {
                return;
            }

            this.PageContext.PageElements.RegisterJsBlockStartup(
                "loadLoginValidatorFormJs",
                JavaScriptBlocks.FormValidatorJs(this.LoginButton.ClientID));

            this.RememberMe.Text = this.GetText("auto");

            this.Password.Attributes.Add(
                "onkeydown",
                JavaScriptBlocks.ClickOnEnterJs(this.LoginButton.ClientID));

            if (this.PageContext.IsGuest && !this.Get<BoardSettings>().DisableRegistrations && !Config.IsAnyPortal)
            {
                this.RegisterLink.Visible = true;
            }

            this.DataBind();
        }

        /// <summary>
        /// Called when Password Recovery is Clicked
        /// </summary>
        /// <param name="sender">
        /// standard event object sender 
        /// </param>
        /// <param name="e">
        /// event args 
        /// </param>
        protected void PasswordRecovery_Click([NotNull] object sender, [NotNull] EventArgs e)
        {
            BuildLink.Redirect(ForumPages.Account_ForgotPassword);
        }

        /// <summary>
        /// Redirects to the Register Page
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RegisterLinkClick(object sender, EventArgs e)
        {
            BuildLink.Redirect(
                this.Get<BoardSettings>().ShowRulesForRegistration ? ForumPages.RulesAndPrivacy : ForumPages.Account_Register);
        }

        /// <summary>
        /// The sign in.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void SignIn(object sender, EventArgs e)
        {
            if (!this.Page.IsValid)
            {
                return;
            }

            var user = this.Get<IAspNetUsersHelper>().ValidateUser(this.UserName.Text.Trim());

            if (user == null)
            {
                this.PageContext.AddLoadMessage(this.GetText("PASSWORD_ERROR"), MessageTypes.danger);
                return;
            }

            // Valid user, verify password
            var result =
                (PasswordVerificationResult)this.Get<IAspNetUsersHelper>().IPasswordHasher.VerifyHashedPassword(user.PasswordHash, this.Password.Text);

            switch (result)
            {
                case PasswordVerificationResult.Success:
                    this.UserAuthenticated(user);
                    break;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    user.PasswordHash = this.Get<IAspNetUsersHelper>().IPasswordHasher.HashPassword(this.Password.Text);
                    this.UserAuthenticated(user);
                    break;
                default:
                    {
                        // Lockout for 15 minutes if more than 10 failed attempts
                        user.AccessFailedCount++;
                        if (user.AccessFailedCount >= 10)
                        {
                            user.LockoutEndDateUtc = DateTime.UtcNow.AddMinutes(15);

                            // TODO: info user
                        }

                        this.Get<IAspNetUsersHelper>().Update(user);

                        this.PageContext.AddLoadMessage(this.GetText("PASSWORD_ERROR"), MessageTypes.danger);
                        break;
                    }
            }
        }

        /// <summary>
        /// The user authenticated.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        private void UserAuthenticated(AspNetUsers user)
        {
            this.Get<IAspNetUsersHelper>().SignIn(user, this.RememberMe.Checked);

            this.Page.Response.Redirect(this.Request.RawUrl);
        }

        #endregion
    }
}