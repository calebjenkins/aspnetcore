﻿// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR
// CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING
// WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF
// TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR
// NON-INFRINGEMENT.
// See the Apache 2 License for the specific language governing
// permissions and limitations under the License.

using System;
using System.Diagnostics;

namespace Microsoft.Net.Http.Server
{
    internal class ServerSession : IDisposable
    {
        internal unsafe ServerSession()
        {
            ulong serverSessionId = 0;
            var statusCode = UnsafeNclNativeMethods.HttpApi.HttpCreateServerSession(
                UnsafeNclNativeMethods.HttpApi.Version, &serverSessionId, 0);

            if (statusCode != UnsafeNclNativeMethods.ErrorCodes.ERROR_SUCCESS)
            {
                throw new WebListenerException((int)statusCode);
            }

            Debug.Assert(serverSessionId != 0, "Invalid id returned by HttpCreateServerSession");

            Id = new HttpServerSessionHandle(serverSessionId);
        }

        public HttpServerSessionHandle Id { get; private set; }

        public void Dispose()
        {
            Id.Dispose();
        }
    }
}
