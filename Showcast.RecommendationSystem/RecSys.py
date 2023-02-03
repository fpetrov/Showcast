import pandas as pd
import numpy as np

from scipy.sparse import csr_matrix
from sklearn.neighbors import NearestNeighbors


class RecSys:
    def __init__(self, movies_dataframe, ratings_dataframe):
        self.movies = movies_dataframe
        self.ratings = ratings_dataframe

    @classmethod
    def from_csv(cls, dataset_path='Notebooks/RecSys/Datasets/', movies_name='movies.csv', ratings_name='ratings.csv'):
        return cls(pd.read_csv(dataset_path + movies_name), pd.read_csv(dataset_path + ratings_name))

    def train(self):
        self.__prepare_data__()

        self.knn = NearestNeighbors(metric='cosine', algorithm='brute', n_neighbors=20, n_jobs=-1)

        self.knn.fit(self.csr_data)

    def evaluate(self, movies_names, recommendations=10):
        movie_search = self.movies[self.movies['title'] == movies_names]

        movie_id = movie_search.iloc[0]['movieId']

        movie_id = self.user_item_matrix[self.user_item_matrix['movieId'] == movie_id].index[0]

        distances, indices = self.knn.kneighbors(self.csr_data[movie_id], n_neighbors=recommendations + 1)

        indices_list = indices.squeeze().tolist()
        distances_list = distances.squeeze().tolist()

        rec_list = self.__prepare_recommendation_list__(distances_list, indices_list)

        rec_df = pd.DataFrame(rec_list, index=range(1, recommendations + 1))

        return rec_df

    def __prepare_recommendation_list__(self, distances_list, indices_list):
        indices_distances = list(zip(indices_list, distances_list))
        indices_distances_sorted = sorted(indices_distances, key=lambda x: x[1], reverse=False)[1:]

        for pair in indices_distances_sorted:
            matrix_movie_id = self.user_item_matrix.iloc[pair[0]]['movieId']

            id = self.movies[self.movies['movieId'] == matrix_movie_id].index

            title = self.movies.iloc[id]['title'].values[0]
            dist = pair[1]

            yield {'Title': title, 'Distance': dist}

    def __prepare_data__(self):
        self.movies.drop(['genres'], axis=1, inplace=True)
        self.ratings.drop(['timestamp'], axis=1, inplace=True)

        self.user_item_matrix = self.ratings.pivot(index='movieId', columns='userId', values='rating')
        self.user_item_matrix.fillna(0, inplace=True)

        users_votes = self.ratings.groupby('userId')['rating'].agg('count')
        movies_votes = self.ratings.groupby('movieId')['rating'].agg('count')

        user_mask = users_votes[users_votes > 50].index
        movie_mask = movies_votes[movies_votes > 10].index

        self.user_item_matrix = self.user_item_matrix.loc[movie_mask, :]
        self.user_item_matrix = self.user_item_matrix.loc[:, user_mask]

        self.csr_data = csr_matrix(self.user_item_matrix.values)
        self.user_item_matrix = self.user_item_matrix.rename_axis(None, axis=1).reset_index()
