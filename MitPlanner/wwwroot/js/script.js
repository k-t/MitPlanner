function initTimelineItemDragEvents() {
    let elements = document.querySelectorAll('.tl-item');
    for (var element of elements) {
        // allow to drop elements to timeline item
        element.addEventListener('dragover', defaultDragOverEventHandler);
        // satisfy Firefox requirements to enable drag and drop (???)
        element.addEventListener('dragstart', defaultDragStartEventHandler)
    }
}

function defaultDragOverEventHandler(e) {
    e.preventDefault();
}

function defaultDragStartEventHandler(e) {
    e.dataTransfer.setData('', event.target.id);
}