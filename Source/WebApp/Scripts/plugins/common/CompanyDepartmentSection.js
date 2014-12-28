//***************************************************************************************************************
//Begin Company - Deparment (Level1) - Section(Level 2) **********************************************************
//****************************************************************************************************************
$(document).on('change', '#LSCompanyID', function () {
    $(this).find("option[value=" + $(this).val() + "]").attr('selected', true).siblings().attr('selected', false);
    PopulateDepartmentsToDropDownList();
    $('#LSLevel1ID').trigger("change");
});

$(document).on('change', '#LSLevel1ID', function () {
    $(this).find("option[value=" + $(this).val() + "]").attr('selected', true).siblings().attr('selected', false);
    PopulateSectionsToDropDownList();
    $('#LSLevel2ID').trigger("change");
});

function PopulateDepartmentsToDropDownList() {
    var select = $("#LSLevel1ID");
    select.empty();
    var LSCompanyID = parseInt($("#LSCompanyID").val());
    var param_data = { LSCompanyID: LSCompanyID };
    if (LSCompanyID > 0) {
        $.getJSON("/Admin/LS_tblLevel1/GetDepartmentsByCompanyID", param_data,
         function (classesData) {
             if (classesData.length > 0) {
                 select.append($('<option/>', { value: '', text: " --- Select --- " }));
                 $.each(classesData, function (index, itemData) {
                     select.append($('<option/>', {
                         value: itemData.Value,
                         text: itemData.Text
                     }));
                 });
                 //select.find('option:first').attr("selected", "selected");
                 PopulateSectionsToDropDownList();
             } else {
                 select.append($('<option/>', { value: '', text: " --- None --- " }));
             }
         });
    } else
        select.append($('<option/>', { value: '', text: " --- None --- " }));

}

function PopulateSectionsToDropDownList() {
    var select = $("#LSLevel2ID");
    select.empty();
    var LSLevel1ID = parseInt($("#LSLevel1ID").val());
    var param_data = { LSLevel1ID: LSLevel1ID };
    if (LSLevel1ID > 0) {
        $.getJSON("/Admin/LS_tblLevel2/GetTeamGroupsByDepartmentID", param_data,
          function (classesData) {
              if (classesData.length > 0) {
                  select.append($('<option/>', { value: '', text: " --- Select --- " }));
                  $.each(classesData, function (index, itemData) {
                      select.append($('<option/>', {
                          value: itemData.Value,
                          text: itemData.Text
                      }));
                  });
                  //select.find('option:first').attr("selected", "selected");
              } else {
                  select.append($('<option/>', { value: '', text: " --- None --- " }));
              }
          });
    } else
        select.append($('<option/>', { value: '', text: " --- None --- " }));
}
//****************************************************************************************************************
//End Company - Deparment (Level1) - Section(Level 2) ************************************************************
//****************************************************************************************************************