
$(document).ready(function () {
    $(function () {

        options = {

            // Required. Called when a user selects an item in the Chooser.
            success: function (files) {
                var f = files[0].link;
                document.getElementById('video').value = f;
            },
            cancel: function () {

            },
 
            linkType: "preview", 
            multiselect: false, 
            folderselect: false
        };
        var button = Dropbox.createChooseButton(options);
        button.innerHTML = '<span class="dropin-btn-status"></span>Subir';
        document.getElementById("video_div").appendChild(button);
    });
    $(function () {
        $('#birth_date').datepicker({
            format: 'dd/mm/yyyy'
        });
    });
    $('#next-country').click(function (e) {
        var page = parseInt($('#next-country-count').val(), 10);
        page = page + 1;
        
        //$('#next-pais-count').val(page);
        document.getElementById("next-country-count").value = page;
        $.ajax({
            url: '/Country/Navigate',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-country').html(data);
                $('#bar-confirm').css('display', 'none');
                $.ajax({
                    url: '/Country/get_n_c',
                    success: function (data) {
                        if(page>=data){
                            document.getElementById("next-country-count").value = data;
                        }
                    }
                });
            }
        });
    });
    $('#prev-country').click(function (e) {
        var page = parseInt($('#next-country-count').val(), 10);
        page = page - 1;
        if (page <= 0) {
            page = 1;
        }
        //$('#next-pais-count').val(page);
        document.getElementById("next-country-count").value = page;
        $.ajax({
            url: '/Country/Navigate',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-country').html(data);
                $('#bar-confirm').css('display', 'none');
                
            }
        });
    });
    $('#first-country').click(function (e) {
        var page = 1;
        //$('#next-pais-count').val(page);
        document.getElementById("next-country-count").value = page;
        $.ajax({
            url: '/Country/Navigate',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-country').html(data);
                $('#bar-confirm').css('display', 'none');

            }
        });
    });
    $('#last-country').click(function (e) {
        var page = -1;
        //$('#next-pais-count').val(page);
        document.getElementById("next-country-count").value = page;
        $.ajax({
            url: '/Country/Navigate',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-country').html(data);
                $('#bar-confirm').css('display', 'none');
                $.ajax({
                    url: '/Country/get_n_c',
                    success: function (data) {
                        document.getElementById("next-country-count").value = data;
                    }
                });
            }
        });
    });

    $('#next-person').click(function (e) {
        var page = parseInt($('#next-person-count').val(), 10);
        page = page + 1;
        //$('#next-pais-count').val(page);
        document.getElementById("next-person-count").value = page;
        $.ajax({
            url: '/Country/NavigatePerson',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-person').html(data);
                $.ajax({
                    url: '/Country/get_n_p',
                    success: function (data) {
                        if(page>=data){
                            document.getElementById("next-person-count").value = data;
                        }
                    }
                });
            }
        });
    });
    $('#prev-person').click(function (e) {
        var page = parseInt($('#next-person-count').val(), 10);
        page = page - 1;
        if (page <= 0) {
            page = 1;
        }
        //$('#next-pais-count').val(page);
        document.getElementById("next-person-count").value = page;
        $.ajax({
            url: '/Country/NavigatePerson',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-person').html(data);
            }
        });
    });

    $('#first-person').click(function (e) {
      
        var page = 1;
        //$('#next-pais-count').val(page);
        document.getElementById("next-person-count").value = page;
        $.ajax({
            url: '/Country/NavigatePerson',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-person').html(data);
            }
        });
    });
    $('#last-person').click(function (e) {

        var page = -1;
        //$('#next-pais-count').val(page);
        document.getElementById("next-person-count").value = page;
        $.ajax({
            url: '/Country/NavigatePerson',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-person').html(data);
                $.ajax({
                    url: '/Country/get_n_p',
                    success: function (data) {
                        document.getElementById("next-person-count").value = data;
                    }
                });
            }
        });
    });

    $('#next-query-all').click(function (e) {
        var page = parseInt($('#query-all-count').val(), 10);
        page = page + 1;
        //$('#next-pais-count').val(page);
        document.getElementById("query-all-count").value = page;
        $.ajax({
            url: '/Queries/NavigatePeopleYearAll',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-query-all').html(data);
            }
        });
    });
    $('#prev-query-all').click(function (e) {
        var page = parseInt($('#query-all-count').val(), 10);
        page = page - 1;
        //$('#next-pais-count').val(page);
        document.getElementById("query-all-count").value = page;
        $.ajax({
            url: '/Queries/NavigatePeopleYearAll',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#partial-query-all').html(data);
            }
        });
    });


    $('#next-info').click(function (e) {
        var page = parseInt($('#info-count').val(), 10);
        page = page + 1;
        //$('#next-pais-count').val(page);
        document.getElementById("info-count").value = page;
        $.ajax({
            url: '/Queries/NavigateInfo',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#div-info').html(data);
            }
        });
    });
    $('#prev-info').click(function (e) {
        var page = parseInt($('#info-count').val(), 10);
        page = page - 1;
        //$('#next-pais-count').val(page);
        document.getElementById("info-count").value = page;
        $.ajax({
            url: '/Queries/NavigateInfo',
            data: { page: page },
            dataType: 'html',
            success: function (data) {
                $('#div-info').html(data);
            }
        });
    });


    $('#next-query-1').click(function (e) {
        var page = parseInt($('#query-1-count').val(), 10);
        page = page + 1;
        document.getElementById("query-1-count").value = page;
        var id_c = document.getElementById('id-country').value;
        if (id_c === '') {

        } else {
            document.getElementById("query-1-count").value = page;
            $.ajax({
                url: '/Queries/NavigatePeopleYear',
                data: { id:id_c, page: page },
                dataType: 'html',
                success: function (data) {
                    $('#partial-query-1').html(data);
                }
            });
        }
        //$('#next-pais-count').val(page);
        
    });
    $('#prev-query-1').click(function (e) {
        var page = parseInt($('#query-1-count').val(), 10);
        page = page - 1;
        //$('#next-pais-count').val(page);
        document.getElementById("query-1-count").value = page;
        var id_c = document.getElementById('id-country').value;
        if (id_c === '') {

        } else {
            document.getElementById("query-1-count").value = page;
            $.ajax({
                url: '/Queries/NavigatePeopleYear',
                data: { id: id_c, page: page },
                dataType: 'html',
                success: function (data) {
                    $('#partial-query-1').html(data);
                }
            });
        }
    });
    $('#query-1').click(function (e) {
        //$('#next-pais-count').val(page);
        var page = 1;
        document.getElementById("query-1-count").value = 1;
        document.getElementById('id-country').value = document.getElementById('country_1').value;
        var id_c = document.getElementById('id-country').value;
        if (id_c === '') {

        } else {
            document.getElementById("query-1-count").value = page;
            $.ajax({
                url: '/Queries/NavigatePeopleYear',
                data: { id: id_c, page: page },
                dataType: 'html',
                success: function (data) {
                    $('#partial-query-1').html(data);
                }
            });
        }
    });

    $('#partial-country').on('click', '.edit2', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id2 = $(this).data("value");
        //alert(id2);
        $.ajax({

            type: 'POST',

            url: '/Country/LoadCountry',
            dataType: 'json',
            data: { id: id2 },
            success: function (data) {
                //alert(data.NBRPAIS);
                $('#country').val(data.COUNTRYNAME);
                $('#area').val(data.AREA);
                $('#population').val(data.POPULATION)
                $('#president').val(data.PRESIDENT);
                $('#flag_photo').attr('src', `data:image/png;base64,${data.FLAGB64}`);
                $('#player').attr('src', `data:audio/ogg;base64,${data.ANTHEMB64}`);
                $('#countryid').val(data.COUNTRYID);
                document.getElementById('old_id').value = data.COUNTRYID;
               
                $('#edit-country-div').css('display', 'block');
                $('#add-country-div').css('display', 'none');
               
            },
            error: function (ex) {
                alert('Failed to retrieve Sub Categories : ' + ex);
            }
        });
        return false;
    });
    $('#partial-person').on('click', '.edit3', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id2 = $(this).data("value");
        //alert(id2);
        $.ajax({

            type: 'POST',

            url: '/Country/LoadPerson',
            dataType: 'json',
            data: { id: id2 },
            success: function (data) {
                //alert(data.NBRPAIS);
                $('#person_name').val(data.NAME);
                $('#birth_list').val(data.BIRTH_COUNTRY_ID);
                $('#residence_list').val(data.RESIDENCE_COUNTRY_ID);
                $('#identification').val(data.IDENTIFICATION);
                document.getElementById('old_id_person').value = data.IDENTIFICATION;
                $('#person_photo').attr('src', `data:image/png;base64,${data.PHOTOB64}`);
               
                $('#birth_date').val(data.DATE);
                $('#email').val(data.EMAIL);
                $('#video').val(data.VIDEO);

                $('#edit-person-div').css('display', 'block');
                $('#add-person-div').css('display', 'none');
            },
            error: function (ex) {
                alert('Failed to retrieve Sub Categories : ' + ex);
            }
        });
        return false;
    });

    $('#partial-person').on('click', '.delete3', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id2 = $(this).data("value");

        swal({
            title: "¿Está de acuerdo con eliminarlo?",
            text: "Está acción será irreversible.",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sí, bórralo"
        },
            function () {
                window.location.replace("/Country/DeletePerson?id=" + id2);
            })

        //alert(id2);
       
        return false;
    });
    $('#partial-country').on('click', '.delete2', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id2 = $(this).data("value");

        swal({
            title: "¿Está de acuerdo con eliminarlo?",
            text: "Está acción será irreversible.",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Sí, bórralo"
        },
            function () {
                window.location.replace("/Country/DeleteCountry?id=" + id2);
            })

        //alert(id2);

        return false;
    });
    $('#cancel-person').click(function (e) {

        e.preventDefault();
        cleanPerson();

        return false;
    });
    $('#cancel').click(function (e) {

        e.preventDefault();
        cleanCountry();

        return false;
    });
    $('.button-control').click(function (e) {

        e.preventDefault();
        e.stopPropagation();

        return false;
    });
    $('#get_flag_file').click(function (e) {
        e.preventDefault();
        document.getElementById('flag_file').click();

      
    });
    $('#get_person_file').click(function (e) {
        e.preventDefault();
        document.getElementById('person_file').click();
    });
    $('#get_anthem_file').click(function (e) {
        e.preventDefault();
        document.getElementById('anthem_file').click();


    });
    $('#play-video').click(function (e) {
        e.preventDefault();
        var videoURL = document.getElementById('video').value;
        if (videoURL === "") {

        } else {
            window.open(videoURL);
        }
    });
    //$('#add').click(function (e) {
    //    e.preventDefault();
      

    //    var form = $('#frmInsertCountry')[0];
    //    var dataString = new FormData(form);

    //    $.ajax({
    //        url: 'Country/PostFiles',
    //        type: 'POST',
    //        xhr: function () {
    //            var myXhr = $.ajaxSettings.xhr();
    //            if (myXhr.upload) {
    //                myXhr.upload.addEventListener('progress', progressHandlingFunction, false);
    //            }
    //            return myXhr;
    //        },
    //        data: dataString,
    //        cache: false,
    //        contentType: false,
    //        processData: false,
    //        success: function (data) {
    //            if ($('#index').val() === "1") {
    //                $('#table_body').empty();
    //                $('#temp-data').empty();
    //                $('#table_body').append(data);
    //            } else {
    //                $('#table_body').append(data);
    //            }
    //            addForName();
    //            var index = parseInt($('#index').val(), 10);
    //            index = index + 1;
    //            document.getElementById("index").value = index;
    //        }
    //    });
    //});
    function progressHandlingFunction(e) {
        if (e.lengthComputable) {
            var percentComplete = Math.round(e.loaded * 100 / e.total);
            $('#progress-bar').css('width', percentComplete + "%");
            if (percentComplete == 100) {
                var delayInMilliseconds = 800;

                setTimeout(function () {
                    $('#progress-bar').css('width', 0 + "%");
                }, delayInMilliseconds);
            }
        }
        else {
        }
    }
    function addForName() {
        var _country = $('#country');
        var _population = $('#population');
        var _president = $('#president');
        var _area = $('#area');
        var _id = $('#countryid');

        var country = createInput('country', _country.val());
        var population = createInput('population', _population.val());
        var president = createInput('president', _president.val());
        var area = createInput('area', _area.val());
        var index = createInput('index_row', $('#index').val());
        var id = createInput('countryid', _id.val());

        var row = $("<div/>", {
            id: 'row-div-' + $('#index').val()
        });
        row.append(country);
        row.append(population);
        row.append(president);
        row.append(area);
        row.append(index);
        row.append(id);

        _country.val('');
        _country.val('');
        _population.val('');
        _president.val('');
        _area.val('');

        $('#temp-data').append(row);
        $('#bar-confirm').css('display', 'block');

    }

    function cleanPerson() {
        $('#person_name').val('');
        $('#identification').val('');
        $('#person_photo').attr('src', '');

        $('#birth_date').val('');
        $('#email').val('');
        $('#video').val('');

        $('#edit-person-div').css('display', 'none');
        $('#add-person-div').css('display', 'block');
    }
    function cleanCountry() {
        $('#countryid').val('');
        $('#country').val('');
        $('#area').val('');
        $('#population').val('');
        $('#president').val('');
        $('#player').attr('src', '');
        $('#flag_photo').attr('src', '');
      

        $('#edit-country-div').css('display', 'none');
        $('#add-country-div').css('display', 'block');
    }
    function createInput(name,value) {
        var input = $("<input/>", {
            type: "text",
            id: name,
            name: name,
            value: value,
        });
        return input;
    }
    $('#partial-country').on('click', '.delete-temp', function (e) {
        e.preventDefault();
        e.stopPropagation();
        var id2 = $(this).data("value");
        $('#row_' + id2).remove();
        $('#row-div-' + id2).remove();
        //alert(id2);
        //$.ajax({

        //    type: 'POST',

        //    url: '/Country/LoadCountry',
        //    dataType: 'json',
        //    data: { id: id2 },
        //    success: function (data) {
        //        //alert(data.NBRPAIS);
        //        $('#country').val(data.COUNTRYNAME);
        //        $('#area').val(data.AREA);
        //        $('#population').val(data.POPULATION);
        //        $('#president').val(data.PRESIDENT);
        //        $('#flag_photo').attr('src', `data:image/png;base64,${data.FLAGB64}`);
        //        $('#add').hide();
        //        $('#edit').removeAttr("hidden");
        //        $('#cancel').removeAttr("hidden");
        //    },
        //    error: function (ex) {
        //        alert('Failed to retrieve Sub Categories : ' + ex);
        //    }
        //});
        return false;
    });
   
});


window.addEventListener('load', function () {
    document.getElementById('flag_file').addEventListener('change', function () {
        if (this.files && this.files[0]) {
            var img = document.getElementById('flag_photo');  // $('img')[0]
            img.src = URL.createObjectURL(this.files[0]); // set src to file url

        } else {
            var img = document.getElementById('flag_photo');  // $('img')[0]
            img.src = '';
        }
    });
    document.getElementById('person_file').addEventListener('change', function () {
        if (this.files && this.files[0]) {
            var img = document.getElementById('person_photo');  // $('img')[0]
            img.src = URL.createObjectURL(this.files[0]); // set src to file url

        } else {
            var img = document.getElementById('person_photo');  // $('img')[0]
            img.src = '';
        }
    });
    document.getElementById('anthem_file').addEventListener('change', function () {
        if (this.files && this.files[0]) {
            var img = document.getElementById('player');  // $('img')[0]
            img.src = URL.createObjectURL(this.files[0]); // set src to file url
            var name = document.getElementById('FileName');
            name.innerHTML = this.files[0].name;
            name.title = this.files[0].name;

        }
    });
});