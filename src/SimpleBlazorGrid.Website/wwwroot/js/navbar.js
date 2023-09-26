function toggleMobileMenu() {
    var x = document.getElementById("navbar");
    if (x.className === "navbar") {
        x.className += " responsive";
    } else {
        x.className = "navbar";
    }
}

function toggleDocumentationMenu() {
    const sidebar = document.getElementById("sidebar");
    const sidebarToggle = document.getElementById("sidebar-toggle");
    const content = document.getElementById("documentation");
    
    if (sidebar.classList.contains('sidebar-collapsed'))
    {
        sidebar.classList.remove('sidebar-collapsed');
        sidebarToggle.classList.remove('sidebar-toggle-collapsed');
        content.classList.remove('documentation-wrapper-collapsed');
    }
    else
    {
        sidebar.classList.add('sidebar-collapsed');
        sidebarToggle.classList.add('sidebar-toggle-collapsed');
        content.classList.add('documentation-wrapper-collapsed');
    }
}