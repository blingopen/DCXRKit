from sklearn import datasets
digits=datasets.load_digits()
print(digits.data.shape)
import matplotlib.pyplot as plt
plt.gray()
plt.matshow(digits.images[0])
plt.show()

digits=datasets.load_digits()
digits.keys()
n_samples,n_features=digits.data.shape
print((n_samples,n_features))

print(digits.data.shape)
print(digits.images.shape)

import numpy as np
print(np.all(digits.images.reshape((1797,64))==digits.data))

fig=plt.figure(figsize=(6,6))
fig.subplots_adjust(left=0,right=1,bottom=0,top=1,hspace=0.05,wspace=0.05)


for i in range(64):
    ax=fig.add_subplot(8,8,i+1,xticks=[],yticks=[])
    ax.imshow(digits.images[i],cmap=plt.cm.binary,interpolation='nearest')
    
    
    ax.text(0,7,str(digits.target[i]))
plt.show()
