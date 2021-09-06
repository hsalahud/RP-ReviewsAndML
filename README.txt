I took the movie web app and added a one to many relationship with reviews/comments. So now we can have one movie with many comments. This app is supposed to be a place where people can leave their comments on a movie, similar to IMDB or Rotten Tomatoes. The application also detects sentiment using Machine Learning when the user first creates a review. The model predicts sentiment at 76% so the accuracy is decent but not perfect. So if the review is positive, an arrow will point to a love it emoji, an angry emoji if they don't write a good comment, and a neutral emoji otherwise.

1. Interactive - CRUD app and typing comments and viewing sentiment tracking is interactive
2. Data persistence - CRUD operations affect an SQL database, which keeps its state when the application is restarted.
3. Library Usage: Used ML.net to build a predictive ML model and used Microsoft.Extensions.ML to access the model in the web application.
4. Creativity: I added the two tables with a one to many relationship and used ML to track sentiment of user's comments.
5. Correctness: All the features above work correctly.