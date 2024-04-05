function showModal(id)
{
    $(`#${id}`).modal("show");
}

function ajaxPostAsync(formId, url, redirect) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url,
            type: "POST",
            data: $(`#${formId}`).serialize(),
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            success: () => {
                resolve();
            },
            error: xhr => {
                reject(xhr.responseText);
            }
        });
    });
}