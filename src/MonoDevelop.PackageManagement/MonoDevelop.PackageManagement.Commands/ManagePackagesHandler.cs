﻿// 
// ManagePackage.cs
// 
// Author:
//   Matt Ward <ward.matt@gmail.com>
// 
// Copyright (C) 2013 Matthew Ward
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using ICSharpCode.PackageManagement;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Projects;
using NuGet;

namespace MonoDevelop.PackageManagement.Commands
{
	public class ManagePackagesHandler : CommandHandler
	{
		protected override void Update (CommandInfo info)
		{
			if (IsDotNetProjectSelected () || IsDotNetSolutionSelected()) {
				info.Bypass = false;
			} else {
				info.Enabled = false;
				info.Bypass = true;
			}
		}
		
		bool IsDotNetProjectSelected ()
		{
			return IdeApp.ProjectOperations.CurrentSelectedProject is DotNetProject;
		}
		
		bool IsDotNetSolutionSelected ()
		{
			return IdeApp.ProjectOperations.CurrentSelectedSolution != null;
		}
		
		protected override void Run ()
		{
			try {
				var viewModels = new PackageManagementViewModels ();
				IPackageManagementEvents packageEvents = PackageManagementServices.PackageManagementEvents;
				var dialog = new ManagePackagesDialog (viewModels.ManagePackagesViewModel, packageEvents);
				MessageService.ShowCustomDialog (dialog);
			} catch (Exception ex) {
				MessageService.ShowException (ex);
			}
		}
	}
}
