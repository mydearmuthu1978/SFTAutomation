$(document).ready(function () {
    $(document).on("click", ".classAdd", function () { //on is used for getting click event for dynamically created buttons

        var rowCount = $('.Task-Details').length + 1;
        var contactdiv = '<tr class="Task-Details">' +
            '<td><input type="text" name="Task' + rowCount + '" class="form-control Task01" /></td>' +
            '<td><input type="text" name="Time-Taken' + rowCount + '" class="form-control Time-Taken01" /></td>' +
            '<td><button type="button" id="btnAdd" class="btn btn-xs btn-primary classAdd">Add More</button>' +
            '<button type="button" id="btnDelete" class="deleteTask btn btn btn-danger btn-xs">Remove</button></td>' +
            '</tr>';
        $('#maintable').append(contactdiv); // Adding these controls to Main table class
    });

    $(document).on("click", ".deleteTask", function () {
        $(this).closest("tr").remove(); // closest used to remove the respective 'tr' in which I have my controls 
    });
});
