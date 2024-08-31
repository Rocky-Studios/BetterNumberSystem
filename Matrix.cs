using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterNumberSystem.Expression;

namespace BetterNumberSystem
{
    /// <summary>
    /// A rectangular array of mathematical objects
    /// </summary>
    public class Matrix
    {
        private IExpressionValue[,] _data;
        public int Rows { get; }
        public int Columns { get; }
        /// <summary>
        /// Initialize an empty matrix
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public Matrix(int rows, int columns)
        {
            Rows = rows; Columns = columns;
            _data = new Number[Rows, Columns];
        }
        /// <summary>
        /// Set or get a value in the matrix
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public IExpressionValue this[int row, int column]
        {
            get { return _data[row-1, column-1]; }
            set { _data[row-1, column-1] = value; }
        }


        // OPERATIONS FOR LATER
        /*
        function addMatrices(matrixA, matrixB):
            if dimensions of matrixA and matrixB are not equal:
                throw error "Matrices must have the same dimensions for addition"
    
            resultMatrix = create matrix with same dimensions as matrixA
    
            for i from 0 to number of rows in matrixA:
                for j from 0 to number of columns in matrixA:
                    resultMatrix[i][j] = matrixA[i][j] + matrixB[i][j]
    
            return resultMatrix

        function subtractMatrices(matrixA, matrixB):
            if dimensions of matrixA and matrixB are not equal:
                throw error "Matrices must have the same dimensions for subtraction"
    
            resultMatrix = create matrix with same dimensions as matrixA
    
            for i from 0 to number of rows in matrixA:
                for j from 0 to number of columns in matrixA:
                    resultMatrix[i][j] = matrixA[i][j] - matrixB[i][j]
    
            return resultMatrix

        function multiplyMatrices(matrixA, matrixB):
            if number of columns in matrixA is not equal to number of rows in matrixB:
                throw error "Number of columns in the first matrix must be equal to the number of rows in the second matrix"
    
            resultMatrix = create matrix with dimensions (rows of matrixA, columns of matrixB)
    
            for i from 0 to number of rows in matrixA:
                for j from 0 to number of columns in matrixB:
                    resultMatrix[i][j] = 0
                    for k from 0 to number of columns in matrixA:
                        resultMatrix[i][j] += matrixA[i][k] * matrixB[k][j]
    
            return resultMatrix

        function transposeMatrix(matrix):
            resultMatrix = create matrix with dimensions (columns of matrix, rows of matrix)
    
            for i from 0 to number of rows in matrix:
                for j from 0 to number of columns in matrix:
                    resultMatrix[j][i] = matrix[i][j]
    
            return resultMatrix
        */
    }
}
