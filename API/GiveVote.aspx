<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiveVote.aspx.cs" Inherits="API.GiveVote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Oylama Sistemi</title> 
    <link rel="stylesheet" type="text/css" href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.0/css/bootstrap.css">
    <style>
        .text-center {
         text-align:center;
        }
        .vote {
          margin:5px;
        }
        #chart_div:first-child {
                width: 100% !important;
          height: 100% !important;
        }
        .btn {
           min-width:150px
        }
        #candidatesAjax .btn {
            min-width:200px
        }
    </style>
</head>
<body>
   <div class="container" > 
        <div class="row">
           <div class="col-lg-12"> 
               <form id="Form1" runat="server">
                    <asp:Literal ID="ltScripts" runat="server"></asp:Literal>  
                    <div id="chart_div">  
                    </div>  
                </form>
            </div>
        </div>
       
        <div class="row text-center"> 
            <div id="candidatesAjax" class="col-lg-12" >
            </div>
        </div>
        <hr />
        <div class="row text-center">  
            <a href="AddCandidate.aspx"  class="btn btn-success"  > Adaylar sayfası</a> 
        </div>
        <hr />
        <div class="row text-center">   
            <button type="button" class="increaseWidth btn btn-info" >Genişliği arttır</button>
            <button type="button" class="decreaseWidth btn btn-warning" >Genişliği azalt</button> 
            <button type="button" class="increaseHeight btn btn-info" >Yükseklik arttır</button>
            <button type="button" class="decreaseHeight btn btn-warning" >Yükseklik azalt</button> 
        </div>
        <hr />
        <div class="row text-center"> 
            <button type="button" class="deleteAll btn btn-danger" data-id="1">Hepsini sil</button>
            <button type="button" class="delete btn btn-danger" data-id="1">Son eklenen oyu sil</button> 
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />        
    </div>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
    <script src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.0/js/bootstrap.min.js"></script>
    <script>

        $(document).ready(function () {
            var uri = 'api/candidates';
            $.getJSON(uri)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        var val = '<button type="button" class="vote btn btn-primary"  data-id="' + item.Id + '">' + item.Name + '</button>';
                        $('#candidatesAjax').append(val);
                    });
                    $('.vote').click(function () {
                        var candidate_id = $(this).data('id');
                        var uri = 'api/votes';
                        $.ajax({
                            url: uri,
                            type: 'POST',
                            dataType: 'json',
                            data: {
                                Candidate_id: candidate_id,
                                User_id: 1
                            }
                        }).success(function (json) { 
                                var json = JSON.parse(JSON.stringify(eval("(" + json + ")")), "'");
                                setChart(json);
                        }).error(function (xhr, status) {
                            alert('Bir hata olustu ' + xhr.responseText);
                        });
                    });
                });

            $('.delete').click(function () { 
                var uri = 'api/votes/GetDeleteLast';
                $.ajax({
                    url: uri 
                }).success(function (json) {
                    var json = JSON.parse(JSON.stringify(eval("(" + json + ")")), "'");
                    setChart(json);
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });

            $('.deleteAll').click(function () {
                var uri = 'api/votes/GetDeleteAll';
                $.ajax({
                    url: uri
                }).success(function (json) {
                    var json = JSON.parse(JSON.stringify(eval("(" + json + ")")), "'");
                    setChart(json);
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });

            // Width increase
            $('.increaseWidth').click(function () {
                var uri = 'api/bars/GetIncreaseWidth';
                $.ajax({
                    url: uri
                }).success(function (json) {
                    location.reload();
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });

            // Width decrease
            $('.decreaseWidth').click(function () {
                var uri = 'api/bars/GetDecreaseWidth';
                $.ajax({
                    url: uri
                }).success(function (json) {
                    location.reload();
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });

            // Height increase
            $('.increaseHeight').click(function () {
                var uri = 'api/bars/GetIncreaseHeight';
                $.ajax({
                    url: uri
                }).success(function (json) {
                    location.reload();
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });
            // Height decrease
            $('.decreaseHeight').click(function () {
                var uri = 'api/bars/GetDecreaseHeight';
                $.ajax({
                    url: uri
                }).success(function (json) {
                    location.reload();
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });

        }); 
    </script>
</body>
</html>
