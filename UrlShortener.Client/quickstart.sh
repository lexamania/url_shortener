#!/bin/bash
# Quick Start Script for URL Shortener Frontend

echo "════════════════════════════════════════════════════"
echo "  URL Shortener Frontend - Quick Start"
echo "════════════════════════════════════════════════════"
echo ""

# Colors for output
GREEN='\033[0;32m'
BLUE='\033[0;34m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo "📋 Checking prerequisites..."
echo ""

# Check Node.js
if ! command -v node &> /dev/null; then
    echo "❌ Node.js is not installed. Please install Node.js 18+ from https://nodejs.org"
    exit 1
fi

echo -e "${GREEN}✓${NC} Node.js $(node --version) detected"

# Check npm
if ! command -v npm &> /dev/null; then
    echo "❌ npm is not installed"
    exit 1
fi

echo -e "${GREEN}✓${NC} npm $(npm --version) detected"
echo ""

# Navigate to project directory
cd "$(dirname "$0")" || exit 1

echo -e "${BLUE}📦 Installing dependencies...${NC}"
npm install

if [ $? -ne 0 ]; then
    echo "❌ Failed to install dependencies"
    exit 1
fi

echo ""
echo -e "${GREEN}✓ Dependencies installed successfully${NC}"
echo ""

echo "════════════════════════════════════════════════════"
echo "  Ready to Start! Choose an option:"
echo "════════════════════════════════════════════════════"
echo ""
echo -e "${BLUE}1)${NC} Start Development Server (recommended)"
echo -e "${BLUE}2)${NC} Build for Production"
echo -e "${BLUE}3)${NC} Run Tests"
echo -e "${BLUE}4)${NC} Exit"
echo ""

read -p "Enter your choice (1-4): " choice

case $choice in
    1)
        echo ""
        echo -e "${YELLOW}Starting Development Server...${NC}"
        echo "The app will open at: http://localhost:4200"
        echo ""
        npm start
        ;;
    2)
        echo ""
        echo -e "${YELLOW}Building for Production...${NC}"
        npm run build
        echo ""
        echo -e "${GREEN}✓ Build complete!${NC}"
        echo "Output: dist/UrlShortener.Client/"
        ;;
    3)
        echo ""
        echo -e "${YELLOW}Running Tests...${NC}"
        npm test
        ;;
    4)
        echo "Goodbye! 👋"
        exit 0
        ;;
    *)
        echo "❌ Invalid choice"
        exit 1
        ;;
esac
