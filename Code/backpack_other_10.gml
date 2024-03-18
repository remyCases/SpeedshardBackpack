event_inherited()
closeButton = scr_adaptiveCloseButtonCreate(id, (depth - 1), 229, 3)
with (closeButton)
    drawHover = 1
getbutton = scr_adaptiveTakeAllButtonCreate(id, (depth - 1), 230, 27)
with (getbutton)
    owner = other.id
cellsContainer = scr_guiCreateContainer(id, o_guiContainerEmpty, depth, adaptiveOffsetX, adaptiveOffsetY)
cellsRowSize = 7
scr_inventory_add_cells(id, cellsContainer, cellsRowSize, 5)