// Hex math defined here: http://blog.ruslans.com/2011/02/hexagonal-grid-math.html

//  Repo - https://github.com/rrreese/Hexagon.js

function HexagonGrid(canvasId, radius, background, orange, brown, pink) {
    //this.radius = radius;
    this.radius = radius;
    this.background = background;
    this.orange = orange;
    this.brown = brown;
    this.pink = pink;
    this.softWhite = "rgba(103,155,153,0.2)";

    //Player Colors
    this.greenPlayer = "#2C8437";
    this.blackPlayer = "#010101";
    this.bluePlayer = "#021B61";
    this.redPlayer = "#D10F40";
    this.yellowPlayer = "#FFD700";
    this.whitePlayer = "#C4C2B6";



    this.setNewHexDimensions();

    this.canvas = document.getElementById(canvasId);
    this.context = this.canvas.getContext('2d');

    this.canvasOriginX = 0;
    this.canvasOriginY = 0;

    this.canvas.addEventListener("mousedown", this.clickEvent.bind(this), false);
};

HexagonGrid.prototype.setNewHexDimensions = function () {
    this.height = Math.sqrt(3) * this.radius;
    this.width = 2 * this.radius;
    this.side = (3 / 2) * this.radius;
};

HexagonGrid.prototype.drawHexGrid = function (rows, cols, originX, originY, isDebug) {
    this.canvasOriginX = originX;
    this.canvasOriginY = originY;

    var currentHexX;
    var currentHexY;
    var debugText = "";

    var offsetColumn = false;

    for (var col = 0; col < cols; col++) {
        for (var row = 0; row < rows; row++) {

            if (!offsetColumn) {
                currentHexX = (col * this.side) + originX;
                currentHexY = (row * this.height) + originY;
            } else {
                currentHexX = col * this.side + originX;
                currentHexY = (row * this.height) + originY + (this.height * 0.5);
            }

            if (isDebug) {
                debugText = col + "," + row;
            }

            this.drawHex(currentHexX, currentHexY, this.softWhite, debugText);
        }
        offsetColumn = !offsetColumn;
    }
};


HexagonGrid.prototype.drawScaledGrid = function (hexes) {

    var rows = [];
    var cols = [];

    var currentHexX;
    var currentHexY;
    var debugText = "";
    var offsetColumn = false;

    for (var i = 0; i < hexes.length; i++) {
        if (rows.indexOf(hexes[i].x) == -1) {
            rows.push(hexes[i].x);
        }

        if (cols.indexOf(hexes[i].y) == -1) {
            cols.push(hexes[i].y);
        }
    }

    rows.sort();
    cols.sort();

    //figure out radius
    var canvasWidth = 1000;

    console.info('before : ' + this.radius);
    this.radius = (canvasWidth / (cols.length)) / 2;
    console.info('after : ' + this.radius);
    this.setNewHexDimensions();

    this.canvasOriginX = this.radius;
    this.canvasOriginY = this.radius;
    var originX = this.radius;
    var originY = this.radius;


    for (var col = 0; col < cols.length; col++) {
        for (var row = 0; row < rows.length; row++) {

            if (!offsetColumn) {
                currentHexX = (col * this.side) + originX;
                currentHexY = (row * this.height) + originY;
            } else {
                currentHexX = col * this.side + originX;
                currentHexY = (row * this.height) + originY + (this.height * 0.5);
            }

            debugText = cols[col] + "," + rows[row];

            this.drawHex(currentHexX, currentHexY, this.softWhite, debugText);
        }
        offsetColumn = !offsetColumn;
    }



};


HexagonGrid.prototype.drawHexAtColRow = function (column, row, color) {
    this.redrawHexAtColRow(column, row, color, "");
};

HexagonGrid.prototype.redrawHexAtColRow = function (column, row, color, text) {
    var drawy = column % 2 == 0 ? (row * this.height) + this.canvasOriginY : (row * this.height) + this.canvasOriginY + (this.height / 2);
    var drawx = (column * this.side) + this.canvasOriginX;
    this.removeHex(drawx, drawy);
    this.drawHex(drawx, drawy, color, text);
};

HexagonGrid.prototype.removeHex = function (x0, y0) {
    this.context.strokeStyle = this.background;
    this.context.beginPath();
    this.context.moveTo(x0 + this.width - this.side, y0);
    this.context.lineTo(x0 + this.side, y0);
    this.context.lineTo(x0 + this.width, y0 + (this.height / 2));
    this.context.lineTo(x0 + this.side, y0 + this.height);
    this.context.lineTo(x0 + this.width - this.side, y0 + this.height);
    this.context.lineTo(x0, y0 + (this.height / 2));
    this.context.fillStyle = this.background;
    this.context.fill();


    this.context.closePath();
    this.context.stroke();


};

HexagonGrid.prototype.drawHex = function (x0, y0, fillColor, debugText) {
    this.context.strokeStyle = "#003432";
    this.context.setLineDash([5, 2]);
    this.context.beginPath();
    this.context.moveTo(x0 + this.width - this.side, y0);
    this.context.lineTo(x0 + this.side, y0);
    this.context.lineTo(x0 + this.width, y0 + (this.height / 2));
    this.context.lineTo(x0 + this.side, y0 + this.height);
    this.context.lineTo(x0 + this.width - this.side, y0 + this.height);
    this.context.lineTo(x0, y0 + (this.height / 2));

    if (fillColor) {
        this.context.fillStyle = fillColor;
        this.context.fill();
    }

    this.context.closePath();
    this.context.stroke();

    if (debugText) {
        this.context.font = "10px sans serif";
        this.context.fillStyle = "#000";
        this.context.fillText(debugText, x0 + (this.width / 2) - (this.width / 4), y0 + (this.height - 5));
    }
};

//Recusivly step up to the body to calculate canvas offset.
HexagonGrid.prototype.getRelativeCanvasOffset = function () {
    var x = 0, y = 0;
    var layoutElement = this.canvas;
    if (layoutElement.offsetParent) {
        do {
            x += layoutElement.offsetLeft;
            y += layoutElement.offsetTop;
        } while (layoutElement = layoutElement.offsetParent);

        return { x: x, y: y };
    }
}

//Uses a grid overlay algorithm to determine hexagon location
//Left edge of grid has a test to acuratly determin correct hex
HexagonGrid.prototype.getSelectedTile = function (mouseX, mouseY) {

    var offSet = this.getRelativeCanvasOffset();

    mouseX -= offSet.x;
    mouseY -= offSet.y;

    var column = Math.floor((mouseX) / this.side);
    var row = Math.floor(
        column % 2 == 0
            ? Math.floor((mouseY) / this.height)
            : Math.floor(((mouseY + (this.height * 0.5)) / this.height)) - 1);


    //Test if on left side of frame            
    if (mouseX > (column * this.side) && mouseX < (column * this.side) + this.width - this.side) {


        //Now test which of the two triangles we are in 
        //Top left triangle points
        var p1 = new Object();
        p1.x = column * this.side;
        p1.y = column % 2 == 0
            ? row * this.height
            : (row * this.height) + (this.height / 2);

        var p2 = new Object();
        p2.x = p1.x;
        p2.y = p1.y + (this.height / 2);

        var p3 = new Object();
        p3.x = p1.x + this.width - this.side;
        p3.y = p1.y;

        var mousePoint = new Object();
        mousePoint.x = mouseX;
        mousePoint.y = mouseY;

        if (this.isPointInTriangle(mousePoint, p1, p2, p3)) {
            column--;

            if (column % 2 != 0) {
                row--;
            }
        }

        //Bottom left triangle points
        var p4 = new Object();
        p4 = p2;

        var p5 = new Object();
        p5.x = p4.x;
        p5.y = p4.y + (this.height / 2);

        var p6 = new Object();
        p6.x = p5.x + (this.width - this.side);
        p6.y = p5.y;

        if (this.isPointInTriangle(mousePoint, p4, p5, p6)) {
            column--;

            if (column % 2 == 0) {
                row++;
            }
        }
    }

    return { row: row, column: column };
};


HexagonGrid.prototype.sign = function (p1, p2, p3) {
    return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
};

//TODO: Replace with optimized barycentric coordinate method
HexagonGrid.prototype.isPointInTriangle = function isPointInTriangle(pt, v1, v2, v3) {
    var b1, b2, b3;

    b1 = this.sign(pt, v1, v2) < 0.0;
    b2 = this.sign(pt, v2, v3) < 0.0;
    b3 = this.sign(pt, v3, v1) < 0.0;

    return ((b1 == b2) && (b2 == b3));
};

HexagonGrid.prototype.clickEvent = function (e) {
    var mouseX = e.pageX;
    var mouseY = e.pageY;

    var localX = mouseX - this.canvasOriginX;
    var localY = mouseY - this.canvasOriginY;

    var tile = this.getSelectedTile(localX, localY);
    if (tile.column >= 0 && tile.row >= 0) {

        console.info("column : " + tile.column + " :: row : " + tile.row);

        var drawy = tile.column % 2 == 0 ? (tile.row * this.height) + this.canvasOriginY + 6 : (tile.row * this.height) + this.canvasOriginY + 6 + (this.height / 2);
        var drawx = (tile.column * this.side) + this.canvasOriginX;

        //console.info("x : " + drawx + " :: y : " + drawy);

        //this.drawHex(drawx, drawy - 6, "rgba(110,110,70,0.3)", "");
    }
};


var GameTile = function (x, y, color, victoryPoints) {
    this.x = x;
    this.y = y;
    this.color = color;
    this.victoryPoints = victoryPoints;
}


HexagonGrid.prototype.clickyEvent = function (e) {
    var mouseX = e.pageX;
    var mouseY = e.pageY;

    var localX = mouseX - this.canvasOriginX;
    var localY = mouseY - this.canvasOriginY;

    var tile = this.getSelectedTile(localX, localY);
    if (tile.column >= 0 && tile.row >= 0) {

        console.info("column : " + tile.column + " :: row : " + tile.row);

        var drawy = tile.column % 2 == 0 ? (tile.row * this.height) + this.canvasOriginY + 6 : (tile.row * this.height) + this.canvasOriginY + 6 + (this.height / 2);
        var drawx = (tile.column * this.side) + this.canvasOriginX;

        console.info("x : " + drawx + " :: y : " + drawy);

        //this.drawHex(drawx, drawy - 6, "rgba(110,110,70,0.3)", "");
    }
};

//function(column, row, color, text) {
HexagonGrid.prototype.buildGameHex = function (tile) {
    var drawy = tile.x % 2 == 0 ? (tile.y * this.height) + this.canvasOriginY : (tile.y * this.height) + this.canvasOriginY + (this.height / 2);
    var drawx = (tile.x * this.side) + this.canvasOriginX;
    this.removeHex(drawx, drawy);
    //this.drawHex(drawx, drawy, tile.color, tile.victoryPoints);


    this.context.strokeStyle = "#003432";
    this.context.setLineDash([5, 2]);
    this.context.beginPath();
    this.context.moveTo(drawx + this.width - this.side, drawy);
    this.context.lineTo(drawx + this.side, drawy);
    this.context.lineTo(drawx + this.width, drawy + (this.height / 2));
    this.context.lineTo(drawx + this.side, drawy + this.height);
    this.context.lineTo(drawx + this.width - this.side, drawy + this.height);
    this.context.lineTo(drawx, drawy + (this.height / 2));

    this.context.fillStyle = tile.color;
    this.context.fill();

    this.context.closePath();
    this.context.stroke();

    if (tile.VictoryPoints) {
        this.context.font = "20px Open Sans";
        this.context.fillStyle = "#FFD300";

        this.context.fillText(tile.VictoryPoints,
            drawx + (this.width - 20),
            drawy + ((this.height / 2) + 5));
    }


    //orange circle
    if (tile.Orange > 0) {
        this.context.beginPath();
        this.context.restore();
        this.context.strokeStyle = this.orange;
        this.context.setLineDash([]);
        var orangeX = drawx + (this.width - 40);
        var orangeY = drawy + ((this.height / 2) + 5);
        this.context.arc(orangeX, orangeY, 5, 0, Math.PI * 2, false);

        this.context.fillStyle = this.orange;
        this.context.fill();

        this.context.stroke();
    }

    //brown circle
    if (tile.Brown > 0) {
        this.context.beginPath();
        this.context.restore();
        this.context.strokeStyle = this.brown;
        this.context.setLineDash([]);
        var brownX = drawx + (this.width - 40);
        var brownY = drawy + ((this.height / 2) + 25);
        this.context.arc(brownX, brownY, 5, 0, Math.PI * 2, false);
        this.context.fillStyle = this.brown;
        this.context.fill();
        this.context.stroke();
    }

    //pink circle
    if (tile.Pink > 0) {
        this.context.beginPath();
        this.context.restore();
        this.context.strokeStyle = this.pink;
        this.context.setLineDash([]);
        var pinkX = drawx + (this.width - 40);
        var pinkY = drawy + ((this.height / 2) - 20);
        this.context.arc(pinkX, pinkY, 5, 0, Math.PI * 2, false);
        this.context.fillStyle = this.pink;
        this.context.fill();
        this.context.stroke();
    }

    //ALIENS
    if (tile.Aliens > 0) {
        var alienX = drawx + (this.width - 65);
        var alienY = drawy + ((this.height / 2) + 5);
        this.context.strokeStyle = "#000016";
        this.context.beginPath();
        this.context.arc(alienX, alienY, 13, Math.PI / 7, -Math.PI / 7, false);
        this.context.lineTo(alienX - 6, alienY);
        //this.context.lineTo(31, 37);
        this.context.fillStyle = '#242437';
        this.context.fill();
    }


    //Player
    if (tile.Occupied != 'Aliens') {
        var playerX = drawx + (this.width - 65);
        var playerY = drawy + ((this.height / 2) + 5);
        var playerColor = this.getColor(tile.Occupied);
        this.context.strokeStyle = playerColor;
        this.context.beginPath();
        this.context.arc(playerX, playerY, 13, 0, Math.PI * 2, false);
        this.context.lineTo(alienX - 6, alienY);
        //this.context.lineTo(31, 37);
        this.context.fillStyle = playerColor;
        this.context.fill();
    }

}

HexagonGrid.prototype.getColor = function (disc) {
    console.log(disc);

    switch (disc) {
        case 'Black':
            return this.blackPlayer;
        case 'Green':
            return this.greenPlayer;
        case 'White':
            return this.whitePlayer;
        case 'Red':
            return this.redPlayer;
        case 'Blue':
            return this.bluePlayer;
        case 'Yellow':
            return this.yellowPlayer;
        default:
            return this.background;
    }
}
