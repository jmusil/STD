$(document).ready(function () {
    $("#createDialog").hide();
    $("#finishDialog").hide();
    $("#editDialog").hide();
    $("#deleteDialog").hide();


    $("#createDialog").dialog({
        autoOpen: false,
        modal: true,
        title: "Create Task",
        width: 230
    });

    $("#finishDialog").dialog({
        autoOpen: false,
        modal: true,
        title: "Mark Task as Finished",
        width: 300,
        buttons: {
            "Mark as Finished": function () {
                $.ajax({
                    url: "/Task/Finish/" + id,
                    type: "Post",
                    dataType: "json"
                }).done(function () {
                    window.location.reload();
                });
                $(this).dialog("close");
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $("#reopenDialog").dialog({
        autoOpen: false,
        modal: true,
        title: "Reopen Finished Task",
        width: 300,
        buttons: {
            "Reopen": function () {
                $.ajax({
                    url: "/Task/Reopen/" + id,
                    type: "Post",
                    dataType: "json"
                }).done(function () {
                    window.location.reload();
                });
                $(this).dialog("close");
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $("#deleteDialog").dialog({
        autoOpen: false,
        modal: true,
        title: "Delete Task",
        width: 300,
        buttons: {
            "Delete Task": function () {
                $.ajax({
                    url: "/Task/Delete/" + id,
                    type: "Post",
                    dataType: "json"
                }).done(function () {
                    window.location.reload();
                });
                $(this).dialog("close");
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });
    $("#editDialog").dialog({
        autoOpen: false,
        modal: true,
        title: "Edit Task",
        width: 230
    });
});
var id;
$(".deleteTask").click(function () {
    id = $(this).attr("id");
    var taskTitle = $(this).parent().parent().children().first().html();
    $("#deleteDialog").dialog("open");
    $("#deleteDialog").empty().append(taskTitle);
});

$("#createNew").click(function () {
    $.ajax({
        url: "/Task/CreatePartial",
        type: "Get",
        success: function (data) {
            $("#createDialog").dialog("open");
            $("#createDialog").empty().append(data);
        },
        error: function () {
            $("#createDialog").empty().append("Error: No data received");
        }
    });
});

$(".editTask").click(function () {
    id = $(this).attr("id");
    $.ajax({
        url: "/Task/Edit/" + id,
        type: "Get",
        success: function (data) {
            $("#editDialog").dialog("open");
            $("#editDialog").empty().append(data);
        },
        error: function () {
            $("#editDialog").dialog("open");
            $("#editDialog").empty().append("Error: No data received");
        }
    });
});
$(".finishTask").click(function () {
    id = $(this).attr("id");
    $.ajax({
        url: "/Task/Finish/" + id,
        type: "Get",
        success: function (data) {
            $("#finishDialog").dialog("open");
            $("#finishDialog").empty().append(data);
        },
        error: function (data) {
            $("finishDialog").dialog("open");
            $("finishDialog").empty().append("Error: No data received");
        }

    });
});

$(".reopenTask").click(function () {
    id = $(this).attr("id");
    $.ajax({
        url: "/Task/Reopen/" + id,
        type: "Get",
        success: function (data) {
            $("#reopenDialog").dialog("open");
            $("#reopenDialog").empty().append(data);
        },
        error: function (data) {
            $("reopenDialog").dialog("open");
            $("reopenDialog").empty().append("Error: No data received");
        }

    });
});