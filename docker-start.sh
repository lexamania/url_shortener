#!/bin/bash

# Docker Startup Script for URL Shortener
# This script helps manage Docker containers for the application

set_colors() {
    BLUE='\033[0;34m'
    GREEN='\033[0;32m'
    YELLOW='\033[1;33m'
    RED='\033[0;31m'
    NC='\033[0m' # No Color
}

show_menu() {
    set_colors
    clear
    echo -e "${BLUE}╔════════════════════════════════════╗${NC}"
    echo -e "${BLUE}║   URL Shortener Docker Manager     ║${NC}"
    echo -e "${BLUE}╚════════════════════════════════════╝${NC}"
    echo ""
    echo "Choose an option:"
    echo ""
    echo -e "${GREEN}1)${NC} Start all services (build + up)"
    echo -e "${GREEN}2)${NC} Start services (no rebuild)"
    echo -e "${GREEN}3)${NC} Stop all services"
    echo -e "${GREEN}4)${NC} View logs"
    echo -e "${GREEN}5)${NC} Rebuild services"
    echo -e "${GREEN}6)${NC} Restart specific service"
    echo -e "${GREEN}7)${NC} Access database shell"
    echo -e "${GREEN}8)${NC} Clean up (remove containers & volumes)"
    echo -e "${GREEN}9)${NC} Show running containers"
    echo -e "${GREEN}0)${NC} Exit"
    echo ""
}

start_services_build() {
    set_colors
    echo -e "${YELLOW}Starting all services with rebuild...${NC}"
    docker compose up --build
}

start_services() {
    set_colors
    echo -e "${YELLOW}Starting all services...${NC}"
    docker compose up
}

stop_services() {
    set_colors
    echo -e "${YELLOW}Stopping all services...${NC}"
    docker compose down
    echo -e "${GREEN}✓ Services stopped${NC}"
}

view_logs() {
    set_colors
    echo ""
    echo "View logs for:"
    echo "1) All services"
    echo "2) API"
    echo "3) Client"
    echo "4) Database"
    echo "5) Follow all logs"
    echo -n "Choose (1-5): "
    read choice
    
    case $choice in
        1) docker compose logs ;;
        2) docker compose logs api ;;
        3) docker compose logs client ;;
        4) docker compose logs postgres ;;
        5) docker compose logs -f ;;
        *) echo -e "${RED}Invalid choice${NC}" ;;
    esac
}

rebuild_services() {
    set_colors
    echo -e "${YELLOW}Rebuilding all services...${NC}"
    docker compose build --no-cache
    echo -e "${GREEN}✓ Services rebuilt. Run 'Start services' to start them.${NC}"
}

restart_service() {
    set_colors
    echo ""
    echo "Select service to restart:"
    echo "1) API"
    echo "2) Client"
    echo "3) Database"
    echo -n "Choose (1-3): "
    read choice
    
    case $choice in
        1) 
            echo -e "${YELLOW}Restarting API...${NC}"
            docker compose up --build -d api
            ;;
        2) 
            echo -e "${YELLOW}Restarting Client...${NC}"
            docker compose up --build -d client
            ;;
        3) 
            echo -e "${YELLOW}Restarting Database...${NC}"
            docker compose restart postgres
            ;;
        *) echo -e "${RED}Invalid choice${NC}" ;;
    esac
    echo -e "${GREEN}✓ Service restarted${NC}"
}

database_shell() {
    set_colors
    echo -e "${YELLOW}Connecting to PostgreSQL...${NC}"
    docker exec -it url-shortener-db psql -U postgres -d url_shortener
}

cleanup() {
    set_colors
    echo -e "${RED}WARNING: This will remove all containers and volumes!${NC}"
    echo -n "Are you sure? (yes/no): "
    read confirm
    
    if [ "$confirm" = "yes" ]; then
        echo -e "${YELLOW}Cleaning up...${NC}"
        docker compose down -v
        echo -e "${GREEN}✓ Cleanup complete${NC}"
    else
        echo "Cancelled"
    fi
}

show_containers() {
    set_colors
    echo ""
    docker ps --format "table {{.Names}}\t{{.Status}}\t{{.Ports}}"
    echo ""
}

# Main loop
while true; do
    show_menu
    echo -n "Enter option (0-9): "
    read option
    
    case $option in
        1) start_services_build ;;
        2) start_services ;;
        3) stop_services ;;
        4) view_logs ;;
        5) rebuild_services ;;
        6) restart_service ;;
        7) database_shell ;;
        8) cleanup ;;
        9) show_containers ;;
        0) 
            echo -e "${BLUE}Goodbye!${NC}"
            exit 0
            ;;
        *) 
            set_colors
            echo -e "${RED}Invalid option. Please try again.${NC}"
            ;;
    esac
    
    echo ""
    echo -n "Press Enter to continue..."
    read
done
