# Use official Node.js LTS Alpine base image
FROM node:20-alpine AS builder

# Set working directory
WORKDIR /app

# Copy dependencies files
COPY excel-table-web/package.json excel-table-web/package-lock.json /app/

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY  excel-table-web/. .

# Build the Next.js project
RUN npm run build

# Generate static files if needed
RUN npm run export || true

# Production stage
FROM node:20-alpine AS runner

WORKDIR /app

# Install only production dependencies
COPY --from=builder /app/package.json /app/package-lock.json ./
RUN npm install --omit=dev

# Copy the built files and static assets from builder stage
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public
# COPY --from=builder /next.config.ts ./next.config.ts

# Expose port (Next.js default)
EXPOSE 3000

# Run the application
CMD ["npm", "start"]
