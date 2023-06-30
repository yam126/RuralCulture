//api帮助
var apiHelper = {
    //urlHost: 'https://192.168.1.20:7026',   
    //urlBase: 'https://192.168.1.20:7026/api',
    urlHost: 'https://localhost:7026',
    urlBase: 'https://localhost:7026/api',
    //urlHost: 'https://192.168.3.97:7126',
    //urlBase: 'https://192.168.3.97:7126/api',
    getQueryString: function (queryParam) {
        var queryStr = "";
        for (var paramName in queryParam) {
            if (queryParam[paramName] != "")
                queryStr += paramName + "=" + queryParam[paramName] + "&";
        }
        if (queryStr != "")
            queryStr = queryStr.substring(0, queryStr.length - 1);
        return queryStr;
    },
    getArticleId: function (articleId, callback) {
        var vurl = this.urlBase + "/article?ArticleId=" + articleId;
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    getOrganizationChildNode: function (OrgId, callback)
    {
        var vurl = this.urlBase + "/organization/search/child/" + OrgId;
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    getArticleContentById: function (articleId, callback) {
        var vurl = this.urlHost + "/article/" + articleId;
        console.log(vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    queryPageGroup: function (queryPageParm, callback) {
        var vurl = this.urlBase + "/group/page?" + this.getQueryString(queryPageParm);
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: queryPageParm,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    queryPageVWArticle: function (queryPageParam, callback) {
        var vurl = this.urlBase + "/article/view/page?" + this.getQueryString(queryPageParam);
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: queryPageParam,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    queryPageFile: function (queryPageParm, callback) {
        var vurl = this.urlBase + "/fileinfo/page?" + this.getQueryString(queryPageParm);
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: queryPageParm,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    queryPageOrganization: function (queryPageParm, callback)
    {
        var vurl = this.urlBase + "/organization/page?" + this.getQueryString(queryPageParm);
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: queryPageParm,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    getDataByPageNameAndTableName: function (queryParam, callback) {
        var vurl = this.urlBase + "/page/table/column/PageName/TableName?" + this.getQueryString(queryParam);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: queryParam,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    GroupFullTextSearch: function (groupName, callback) {
        var vurl = this.urlBase + "/group/search/name?searchKey=" + groupName;
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: null,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    console.log("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    SearchOrganizationByName: function (OrgName, callback)
    {
        var vurl = this.urlBase + "/organization/search/name?OrgName=" + OrgName;
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: null,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    getOrganizationById: function (OrgId, callback)
    {
        var vurl = this.urlBase + "/organization?groupId=" + OrgId;
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            data: null,
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    saveArticle: function (saveData,saveMethod,callback)
    {
        var vurl = this.urlBase + "/article/save";
        var method = "POST";
        var postData = new FormData();
        var jsonStr = "";
        var htmlCode = "";
        htmlCode = saveData.Content;
        saveData.Content = "";
        jsonStr = JSON.stringify(saveData);
        console.log("jsonStr");
        console.log(jsonStr);
        postData.append("articleInfo", jsonStr);
        postData.append("saveMethod", saveMethod);
        postData.append("htmlCode", htmlCode);
        $.ajax({
            type: method,
            url: vurl,
            async: false,
            //jsonp: "",
            //contentType: "application/json",//设置请求参数类型为json字符串
            //dataType: "json",
            cache: false,//上传文件无需缓存
            processData: false,  // 不处理数据
            contentType: false, // 不设置内容类型
            data: postData,
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });
    },
    saveGroupInfo: function (saveData, saveMethod, callback)
    {
        var vurl = this.urlBase + "/group";
        var method = "POST";
        if (saveMethod == "Add") {
            method = "POST";
        } else if (saveMethod == "Edit") {
            method = "PUT";
            vurl += "/" + saveData.groupId;
        }
        console.log("vurl=" + vurl);
        console.log(JSON.stringify(saveData));
        $.ajax({
            type: method,
            url: vurl,
            async: false,
            jsonp: "",
            contentType: "application/json",//设置请求参数类型为json字符串
            dataType: "json",
            data: JSON.stringify(saveData),
            //data: saveData,
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });

    },
    savePageTableColumn: function (saveData, callback)
    {
        var vurl = this.urlBase + "/page/table/column";
        console.log(vurl);
        console.log(JSON.stringify(saveData));
        $.ajax({
            type: "POST",
            url: vurl,
            async: false,
            jsonp: "",
            contentType: "application/json",//设置请求参数类型为json字符串
            dataType: "json",
            data: JSON.stringify(saveData),
            //data: saveData,
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });
    },
    saveOrganization: function (saveData, saveMethod, callback)
    {
        var vurl = this.urlBase + "/organization";
        var method = "POST";
        var orgId = saveData.orgId;
        console.log(vurl);
        if (saveMethod == "Edit") {
            method = "PUT";
            vurl += "/" + orgId;
        }
        $.ajax({
            type: method,
            url: vurl,
            async: false,
            jsonp: "",
            contentType: "application/json",//设置请求参数类型为json字符串
            dataType: "json",
            data: JSON.stringify(saveData),
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });
    },
    uploadFile: function (file, callback)
    {
        var vurl = this.urlBase + "/fileinfo/uploadfile";
        var postData = new FormData();
        postData.append("file", file);
        console.log("vurl=" + vurl);
        $.ajax({
            type: "POST",
            url: vurl,
            async: false,
            cache: false,//上传文件无需缓存
            processData: false,  // 不处理数据
            contentType: false, // 不设置内容类型
            data: postData,
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });
    },
    deleteArticle: function (IDStr, callback) {
        var vurl = this.urlBase + "/article/" + IDStr;
        $.ajax({
            type: "DELETE",
            url: vurl,
            async: false,
            jsonp: "",
            contentType: "application/json",//设置请求参数类型为json字符串
            //dataType: "json",
            //data: JSON.stringify(IDAry),
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                /*if (response.data.status != 0)
                    alert("删除失败" + response.data.msg);
                else
                    alert("删除成功");*/
                callback(response);
            }
        });
    },
    deleteGroup: function (IDStr, callback) {
        var vurl = this.urlBase + "/group/" + IDStr;
        $.ajax({
            type: "DELETE",
            url: vurl,
            async: false,
            jsonp: "",
            contentType: "application/json",//设置请求参数类型为json字符串
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });
    },
    deleteFile: function (IDStr, callback)
    {
        var vurl = this.urlBase + "/fileinfo/" + IDStr;
        $.ajax({
            type: "DELETE",
            url: vurl,
            async: false,
            jsonp: "",
            contentType: "application/json",//设置请求参数类型为json字符串
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });
    },
    batchDeleteOranization: function (IDStr, callback)
    {
        var vurl = this.urlBase + "/organization?deleteIdStr=" + IDStr;
        console.log("batchDeleteOranization");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "DELETE",
            url: vurl,
            async: false,
            jsonp: "",
            contentType: "application/json",//设置请求参数类型为json字符串
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                callback(response);
            }
        });
    },
    getRecurrenceOrgData: function (OrgId, level, callback)
    {
        var vurl = this.urlBase + "/organization/recurrence?OrgId=" + OrgId + "&level=" + level;
        console.log("getRecurrenceOrgData");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    queryAdvanceArticlePage: function (where, advancedWhere,pageIndex, pageSize, beforeSendBack, callback)
    {
        if (where == '')
            where = "null";
        if (advancedWhere == '')
            advancedWhere = "null";
        var vurl = this.urlBase + '/article/advance/query/page?where=' + where + '&advancedWhere=' + advancedWhere+'&pageIndex=' + pageIndex + '&pageSize=' + pageSize;
        console.log("queryAdvanceArticlePage");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            beforeSend: function ()
            {
                beforeSendBack();
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    queryArticleViewCountTopTen: function (callback)
    {
        var vurl = this.urlBase + "/article/viewcount/topten";
        console.log("queryArticleViewCountTopTen");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    QueryArticleCreatedTimeTop: function (topnum,callback) {
        var vurl = this.urlBase + "/article/createdtime/topten?topnum=" + topnum;
        console.log("QueryArticleCreatedTimeTop");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    QueryArticleTop: function (callback) {
        var vurl = this.urlBase + "/article/top";
        console.log("QueryArticleTop");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    QueryGroupTop: function (callback) {
        var vurl = this.urlBase + "/group/top";
        console.log("QueryGroupTop");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    GetRuralCategorPageData: function (groupId, callback)
    {
        var vurl = this.urlBase + "/article/ruralcategor/ruralcategor?groupId=" + groupId;
        console.log("GetRuralCategorPageData");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    QueryArticleIsSpecial: function (callback)
    {
        var vurl = this.urlBase + "/article/isspecial";
        console.log("QueryArticleIsSpecial");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    },
    QueryAllGroup: function (callback)
    {
        var vurl = this.urlBase + "/group/all";
        console.log("QueryArticleIsSpecial");
        console.log("vurl=" + vurl);
        $.ajax({
            type: "get",
            url: vurl,
            async: false,
            jsonp: "",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Headers": "Authorization"
            },
            success: function (response) {
                console.log(response);
                response.data = response;
                console.log(response);
                if (response.data.status != 0)
                    alert("调用接口出错,原因:\t\r\n" + response.data.msg);
                else
                    callback(response);
            }
        });
    }
}