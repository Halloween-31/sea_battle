export function make_table() {
    let table_field = document.createElement('table');
    let inTable = '<thead><tr> </tr></thead>\n<tbody> </tbody>\n<tfoot><tr> </tr></tfoot>';
    let whereWrite = inTable.indexOf(' ');

    let tmpStr = '';
    tmpStr += '<th> </th>';
    for (let i = 0; i < 10; i++) {
        tmpStr += '<th>' + i + '</th>';
    }

    let start = '';
    for (let i = 0; i < whereWrite; i++) {
        start += inTable[i];
    }
    let end = '';
    for (let i = whereWrite + 1; i < inTable.length; i++) {
        end += inTable[i];
    }
    inTable = '';
    inTable += start;
    inTable += tmpStr;
    inTable += end;
    tmpStr = '';

    for (let i = 0; i < 10; i++) {
        tmpStr += '<tr>';
        tmpStr += '<th>' + i + '</th>';
        for (let j = 0; j < 10; j++) {
            tmpStr += '<td><button> </button></td>';
        }
        tmpStr += '</tr>';
    }

    whereWrite = inTable.indexOf('<tbody>');
    whereWrite += 7;
    start = '';
    for (let i = 0; i < whereWrite; i++) {
        start += inTable[i];
    }
    end = '';
    for (let i = whereWrite + 1; i < inTable.length; i++) {
        end += inTable[i];
    }
    inTable = '';
    inTable += start;
    inTable += tmpStr;
    inTable += end;
    tmpStr = '';

    tmpStr += '<td> </td>'
    for (let i = 0; i < 10; i++) {
        tmpStr += '<td>-</td>'
    }

    whereWrite = inTable.indexOf('<tfoot><tr>');
    whereWrite += 11;
    start = '';
    for (let i = 0; i < whereWrite; i++) {
        start += inTable[i];
    }
    end = '';
    for (let i = whereWrite + 1; i < inTable.length; i++) {
        end += inTable[i];
    }
    inTable = '';
    inTable += start;
    inTable += tmpStr;
    inTable += end;
    tmpStr = null;
    start = null;
    end = null;
    whereWrite = null;

    table_field.innerHTML = inTable;

    let divToInsert = document.querySelector('div > div > div:last-child');

    if (divToInsert != null) {
        divToInsert.append(table_field); // .insertAdjacentHTML('beforeend', table_field); // ніби оцей другий метод кращий

        table_field = document.createElement('table');  // .cloneNode(true) 
        table_field.innerHTML = inTable;
        divToInsert = document.querySelector('div > div > div:first-child');
        divToInsert.append(table_field);
        //divToInsert.insertAdjacentHTML('beforeend', table_field); //не спрацював певно через \n

        table_field = null;
        inTable = null;
        divToInsert = null;
    }
}