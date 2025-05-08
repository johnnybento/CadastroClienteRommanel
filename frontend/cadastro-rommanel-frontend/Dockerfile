# STAGE 1: build do Angular
FROM node:18-alpine AS builder
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build -- --configuration production

# STAGE 2: serve com Nginx
FROM nginx:stable-alpine
COPY --from=builder /app/dist/cadastro-rommanel-frontend /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]