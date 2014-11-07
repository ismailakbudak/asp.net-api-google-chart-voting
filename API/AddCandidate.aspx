<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCandidate.aspx.cs" Inherits="API.AddCandidate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adaylar</title>
    <link rel="stylesheet" type="text/css" href="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.0/css/bootstrap.css">
    <style>
        .btn {
           min-width:150px
        }
    </style>
</head>
<body> 
    
    <div class="container" > 
        <div class="row">  
              <div class="bs-example">
                <h2 style="text-align:center" >Adaylar</h2>
                <hr />
                <div class="row"> 
                    <div class="col-md-4 col-md-offset-4"   >
                        <ul id="candidatesAjax" style="list-style: disc">
                        </ul>  
                    </div>
                </div>
                <hr /> 
                <div class="row">
                    <div class="col-lg-4 col-lg-offset-4" style="text-align:center" > 
                        
                            <div class="form-group"> 
                            <input type="text" class="form-control" id="candidate_name" placeholder="Aday İsim Soyisim">
                            
                            </div> 
                                <button type="button" class="btn btn-primary "  value="Ekle" onclick="add();" >Ekle</button>
                             
                    </div>
               </div>
              </div> 
        </div>
        <hr />
        <div class="row text-center">  
            <a href="GiveVote.aspx"  class="btn btn-success"  > Oy sonuçları sayfası</a>
        </div>
        <hr />
        <div class="row text-center"> 
            <button type="button" class="deleteAll btn btn-danger"  >Hepsini sil</button>
            <button type="button" class="delete btn btn-danger"  >Son eklenen adayı sil</button> 
        </div>
        <br />
        <br />
        <br />
        <br />
    </div>


    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js"></script>
    <script>

        $(document).ready(function () {
            var uri = 'api/candidates';
            $.getJSON(uri)
                .done(function (data) {
                    $.each(data, function (key, item) {
                        $('<li>', { text: item.Name , value: item.Id }).appendTo($('#candidatesAjax'));
                    });
                });
             
            $('.delete').click(function () {
                var uri = 'api/candidates/GetDeleteLast';
                $.ajax({
                    url: uri
                }).success(function (json) {
                    location.reload();
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });

            $('.deleteAll').click(function () {
                var uri = 'api/candidates/GetDeleteAll';
                $.ajax({
                    url: uri
                }).success(function (json) {
                    location.reload();
                }).error(function (xhr, status) {
                    alert('Bir hata olustu ' + xhr.responseText);
                });
            });
        });

        function add() {
            var candidate_name = $("#candidate_name").val();
            var uri = 'api/candidates';
            $.ajax({
                url: uri,
                type: 'POST',
                dataType: 'json',
                data: {
                    name: candidate_name
                }
            }).success(function (result) {
                location.reload();
            }).error(function (xhr, status) {
                alert('Bir hata olustu ' + xhr.responseText);
            });
        }
    </script>

</body>
</html>
